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
            // Inicializar Firebase
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "BF0KL7IhqDzptYihcrOVlWsJ9J0iLYqSMlQvebqV",
                BasePath = "https://warbimpro-default-rtdb.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            // Abrir la ventana de login y pasar el cliente de Firebase
            Logeo loginWindow = new Logeo(client);
            loginWindow.Show();

        }
    }
}
