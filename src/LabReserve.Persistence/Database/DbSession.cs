using System.Data;
using LabReserve.Persistence.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LabReserve.Persistence.Database;

public sealed class DbSession : IDbSession
{
    private readonly IConfiguration _configuration;
    private readonly Guid _id;

    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; set; }

    public DbSession(IConfiguration configuration)
    {
        _id = Guid.NewGuid();
        _configuration = configuration;

        var connectionString = _configuration.GetConnectionString("Default");
        Connection = new SqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();
}