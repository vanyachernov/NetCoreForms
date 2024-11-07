using System.Security.Cryptography;
using System.Text;
using Forms.Application.Providers;

namespace Forms.Infrastructure.Providers;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        
        var result = new StringBuilder(length);
        
        using (var rng = RandomNumberGenerator.Create())
        {
            var uintBuffer = new byte[4];

            while (result.Length < length)
            {
                rng.GetBytes(uintBuffer);
                
                var num = BitConverter.ToUInt32(uintBuffer, 0);
                
                result.Append(validChars[(int)(num % (uint)validChars.Length)]);
            }
        }

        return result.ToString();
    }
}