using ConfigGenerator.Utils.Security;
using System;
using System.IO;
using System.Text.Json;

namespace ConfigGenerator
{
    internal class Program
    {
        static void Main()
        {
            var config = new FirebaseConfigData
            {
                AuthSecret = "9sM2cKwGixqXR1FzxzZa7EyRrgn2INouUfcXFV8h",
                BasePath = "https://warbimpro-default-rtdb.firebaseio.com/"
            };

            string json = JsonSerializer.Serialize(config);
            string encrypted = AesEncryption.Encrypt(json);

            string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            string outputPath = Path.Combine(solutionRoot, "install", "Resources", "config.dat");


            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            File.WriteAllText(outputPath, encrypted);

            Console.WriteLine("✅ Archivo config.dat generado en Installer/Resources");
        }
    }
}
