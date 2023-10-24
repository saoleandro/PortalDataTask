using System.Security.Cryptography;
using System.Text;

namespace PortalDataTask.Infra.CrossCutting.Services.Extensions;

public class PasswordEncryption
{
    public static string GetSHA1(string text)
    {
        SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
        sh.ComputeHash(Encoding.ASCII.GetBytes(text));
        byte[] re = sh.Hash;
        StringBuilder sb = new StringBuilder();
        sb.Append("#enc#");
        foreach (byte b in re)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}
