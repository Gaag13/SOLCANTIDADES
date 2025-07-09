using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;
using Cantidades.Utils;
using Cantidades.Utils.Security;

namespace Cantidades.Security
{
    public class FirebaseConfigData
    {
        public string AuthSecret { get; set; }
        public string BasePath { get; set; }
    }

    public static class FirebaseConfigLoader
    {
        public static IFirebaseConfig LoadConfig()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Cantidades", "config.dat");

            if (!File.Exists(path))
                throw new FileNotFoundException("El archivo config.dat no fue encontrado en %APPDATA%\\Cantidades");

            string encrypted = File.ReadAllText(path);
            string decrypted = AesEncryption.Decrypt(encrypted);

            var configData = System.Text.Json.JsonSerializer.Deserialize<FirebaseConfigData>(decrypted);

            return new FirebaseConfig
            {
                AuthSecret = configData.AuthSecret,
                BasePath = configData.BasePath
            };
        }
    }
}
