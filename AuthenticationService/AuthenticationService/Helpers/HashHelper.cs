using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService.Helpers;

public static class HashHelper
{
    public static string GetHash(string input) => 
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
}