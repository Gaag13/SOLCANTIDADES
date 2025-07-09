using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConfigGenerator.Utils.Security
{
    public static class AesEncryption
    {
        // Debe tener exactamente 16 caracteres (AES-128)
        private static readonly string Key = "WARBIMAESKEY1234";

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.GenerateIV(); // Genera IV aleatorio

                using (MemoryStream ms = new MemoryStream())
                {
                    // Escribir IV primero
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    // Retornar todo como Base64
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);

                // Leer los primeros 16 bytes como IV
                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                // El resto del contenido cifrado
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
