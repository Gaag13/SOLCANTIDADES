using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using CANTIDADES.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using Microsoft.Office.Interop.Excel;

namespace CANTIDADES.Views
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : System.Windows.Window
    {
        private IFirebaseClient client;

        //IFirebaseConfig config = new FirebaseConfig
        //{
        //    AuthSecret = "BF0KL7IhqDzptYihcrOVlWsJ9J0iLYqSMlQvebqV",
        //    BasePath = "https://warbimpro-default-rtdb.firebaseio.com/"
        //};
        //IFirebaseClient client;
        public Register(IFirebaseClient firebaseClient)
        {
            InitializeComponent();
            client = firebaseClient;
            try
            {
                if (client != null)
                {
                    MessageBox.Show("Éxito", "Conexión establecida correctamente con Firebase");
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Error", "Archivo no encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "No se pudo establecer la conexión: " + ex.Message);
            }

        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            var data = new DataFirebase()
            {
                Name = txtNombreUsuario.Text,
                Email = txtEmail.Text,
                Country = txtPais.Text,
                Password = txtPassword.Password
            };


            var response = await client.PushAsync("Usuario/", data);
            // Obtener el ID generado por Firebase
            string generatedId = response.Result.name;  // Firebase retorna el ID generado bajo "name"

            // Mostrar el ID generado o el mensaje de éxito
            MessageBox.Show($"Registro exitoso");

            Logeo loginWindow = new Logeo(client);
            loginWindow.Show();
            this.Close();
        }
    }
}
