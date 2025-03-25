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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.UI;
using CANTIDADES.Commands;
using CANTIDADES.Models;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace CANTIDADES.Views
{
    /// <summary>
    /// Lógica de interacción para Logeo.xaml
    /// </summary>
    public partial class Logeo : Window
    {
        
        private IFirebaseClient client;

        public Logeo(IFirebaseClient firebaseClient)
        {
            InitializeComponent();
            client = firebaseClient;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string enteredUsername = txtLoginUsername.Text;
            string enteredPassword = txtLoginPassword.Password;

            FirebaseResponse response = await client.GetAsync("Usuario");
            Dictionary<string, DataFirebase> allusers = response.ResultAs<Dictionary<string, DataFirebase>>();

            bool userFound = false;
            foreach (var user in allusers)
            {
                if (user.Value.Name == enteredUsername)
                {
                    userFound=true;
                    if (Security.VerificacionCotraseña(enteredPassword,user.Value.Password))
                    {
                        MessageBox.Show("Logueado Exitoso", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        SesionManager guardarEstadoDeSesion=new SesionManager();
                        guardarEstadoDeSesion.GuardarEstadoDeSesion(enteredUsername);
                        TaskDialog.Show("Exito", "Button Habilitado");
                        AvailabilityButton.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                }
            }
            if (!userFound)
            {
                MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void ButtonBaseR_OnClick(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register(client);
            registerWindow.ShowDialog();
            this.Close();
        }
        private void Logeo_OnLoaded(object sender, RoutedEventArgs e)
        {

            SesionManager estadoLogin = new SesionManager();
            var usuarioId = estadoLogin.ObtenerUsuarioActual();

            if (!string.IsNullOrEmpty(usuarioId))
            {
                //ValidarLicencia(usuarioId);
            }
            else
            {
                MessageBox.Show("No se encontró un usuario logueado.");
            }

            if (estadoLogin.EstaLogueado())
            {
                AvailabilityButton.IsEnabled = true;
                MessageBox.Show("Bienvenido de nuevo. Ya estas logueado");
                this.Close();
            }
            else
            {
                MessageBox.Show("No estas logueado");
            }

        }
        //private async void ValidarLicencia(string usuarioId)
        //{
        //    FirebaseResponse response = await client.GetAsync("Usuario");
        //    var licencia = response.ResultAs<DataFirebase>();
        //    if (licencia == null)
        //    {
        //        MessageBox.Show("No se encontró la licencia.");
        //        return;
        //    }
        //    TimeSpan diferencia = DateTime.Now - DateTime.Parse(licencia.Datetime);

        //    if (diferencia.TotalDays > 1 && licencia.IsActive)
        //    {
        //        MessageBox.Show("Tu version de prueba ha expirado. Por favor, compra el producto");
        //        licencia.IsActive = false;
        //        await client.SetAsync($"Licencias/{usuarioId}", licencia);
        //        System.Windows.Application.Current.Shutdown();
        //    }
        //    else
        //    {
        //        MessageBox.Show($"Días restantes: {15 - diferencia.TotalDays}");
        //    }

        //}

        
    }
}
