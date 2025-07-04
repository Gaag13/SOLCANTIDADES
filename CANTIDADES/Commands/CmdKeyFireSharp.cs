using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using CANTIDADES.Models;
using CANTIDADES.Views;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using Nice3point.Revit.Toolkit.External;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;


namespace CANTIDADES.Commands
{
   
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CmdKeyFireSharp : ExternalCommand 
    {
        public override void Execute()
        {
           

            var config = new FirebaseConfig
            {
                AuthSecret = "9sM2cKwGixqXR1FzxzZa7EyRrgn2INouUfcXFV8h",
                BasePath = "https://warbimpro-default-rtdb.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            Logeo loginWindow = new Logeo(client);
            loginWindow.Show();
        }
    }
}
