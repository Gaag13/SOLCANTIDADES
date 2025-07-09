using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cantidades.Utils.Security
{
    public static class AesEncryption
    {
        private static readonly string Key = "WARBIMAESKEY1234"; // ✅ debe coincidir con ConfigGenerator

        public static string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);

                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                int cipherTextOffset = iv.Length;
                int cipherTextLength = fullCipher.Length - cipherTextOffset;
                byte[] cipherBytes = new byte[cipherTextLength];
                Array.Copy(fullCipher, cipherTextOffset, cipherBytes, 0, cipherTextLength);

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
