using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Cantidades.Security;
using CANTIDADES.Models;
using CANTIDADES.Utils;
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
            var authService = new FirebaseAuthService();
            Logeo loginWindow = new Logeo(authService);
            loginWindow.Show();
        }
    }
}
