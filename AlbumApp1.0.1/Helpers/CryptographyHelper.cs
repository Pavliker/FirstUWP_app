using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace AlbumApp1._0._1.Helpers
{
    public static class CryptographyHelper
    {
        public static string HashingPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (SHA512 SHA512 = System.Security.Cryptography.SHA512.Create())
            {
                var hashed = SHA512.ComputeHash(bytes);
                var hashConvertToText = BitConverter.ToString(hashed).Replace("-", "");
                return hashConvertToText.ToString();
            }

    }
}
}
