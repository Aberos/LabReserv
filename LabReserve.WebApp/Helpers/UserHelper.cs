using System.Security.Cryptography;
using System.Text;

namespace LabReserve.WebApp.Helpers;

public static class UserHelper
{
    public static string EncryptPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var t in bytes)
            builder.Append(t.ToString("x2"));
        
        return builder.ToString();
    }
}