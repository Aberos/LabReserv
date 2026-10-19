using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.Data.SqlClient;

namespace LabReserve.Migrations;

public static class DatabaseMigrator
{
    private const int MaxConnectionAttempts = 30;
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(2);

    public static bool Migrate(string connectionString)
    {
        EnsureDatabaseExists(connectionString);

        var result = CreateUpgradeEngine(connectionString).PerformUpgrade();
        if (!result.Successful)
            Console.Error.WriteLine(result.Error);

        return result.Successful;
    }

    private static void EnsureDatabaseExists(string connectionString)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                EnsureDatabase.For.SqlDatabase(connectionString);
                return;
            }
            catch (SqlException) when (attempt < MaxConnectionAttempts)
            {
                Console.WriteLine($"Waiting for SQL Server ({attempt}/{MaxConnectionAttempts})");
                Thread.Sleep(RetryDelay);
            }
        }
    }

    private static UpgradeEngine CreateUpgradeEngine(string connectionString) =>
        DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();
}
