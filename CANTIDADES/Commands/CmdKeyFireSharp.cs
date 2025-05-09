using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using Nice3point.Revit.Toolkit.External;
using CANTIDADES.Models;
using CANTIDADES.Views;
using System.Data.SqlClient;


namespace CANTIDADES.Commands
{
   
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CmdKeyFireSharp : ExternalCommand 
    {
        public override void Execute()
        {
            DotEnvLoader.Load();

            string authSecret = Environment.GetEnvironmentVariable("FIREBASE_SECRET");
            string basePath = Environment.GetEnvironmentVariable("FIREBASE_URL");

            var config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            Logeo loginWindow = new Logeo(client);
            loginWindow.Show();
        }
    }
}
