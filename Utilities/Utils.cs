using System.Text;
using System.Security.Cryptography;

public static class Utils
{
    public static string Encrypt(string password, string salt)
    {
        var passStr = password + salt;
        var crypt = SHA256.Create();
        var hash = new StringBuilder();
        byte[] bytes = crypt.ComputeHash(Encoding.UTF8.GetBytes(passStr));

        foreach (byte thisByte in bytes) {
            hash.Append(thisByte.ToString("x2"));
        }
        return hash.ToString();
    }

    public static string GenerateSalt()
    {
        using (var rng = RandomNumberGenerator.Create())
            {
                var maxLength = 16;
                var salt = new byte[maxLength];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
    }
}