//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Configuration;
//using System.Data.SqlClient;
//using Autodesk.Revit.DB.Architecture;
//using Autodesk.Revit.UI;

//namespace QUANTITIES.Views
//{
//    /// <summary>
//    /// Lógica de interacción para Login.xaml
//    /// </summary>
//    public partial class LoginSql : Window
//    {
//        public LoginSql()
//        {
//            InitializeComponent();
            
//        }
//        private void Button_Registrarse_Click(object sender, RoutedEventArgs e)
//        {
//            // Validar que los campos no estén vacíos
//            if (string.IsNullOrEmpty(txtNombreUsuario.Text) || string.IsNullOrEmpty(txtPais.Text) ||
//                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password))
//            {
//                MessageBox.Show("Por favor, rellene todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                return;
//            }

//            // Intentar conectar y registrar los datos
//            string connectionString = "Data Source=ADMIN;Initial Catalog=REGISTROAPP;Integrated Security=True;TrustServerCertificate=True";
//            using (SqlConnection con = new SqlConnection(connectionString))  // using garantiza que la conexión se cierre correctamente
//            {
//                try
//                {
//                    con.Open();  // Abrir conexión

//                    // Consulta de inserción
//                    string insertQuery = "INSERT INTO RWARBIMPRO (Nombre, Pais, Correo, Cotrasena) VALUES (@Nombre, @Pais, @Correo, @Cotrasena)";
//                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
//                    {
//                        // Asignar los valores de los campos como parámetros
//                        cmd.Parameters.AddWithValue("@Nombre", txtNombreUsuario.Text);
//                        cmd.Parameters.AddWithValue("@Pais", txtPais.Text);
//                        cmd.Parameters.AddWithValue("@Correo", txtEmail.Text);
//                        cmd.Parameters.AddWithValue("@Cotrasena", txtPassword.Password);

//                        // Ejecutar el comando
//                        cmd.ExecuteNonQuery();
//                    }

//                    // Mostrar mensaje de éxito
//                    MessageBox.Show("Registro exitoso.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                catch (SqlException ex)
//                {
//                    // Capturar y mostrar cualquier error que ocurra durante la inserción
//                    MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }

//            //using (SqlConnection con = new SqlConnection(connectionString))
//            //{
//            //    try
//            //    {
//            //        con.Open();  // Intentar abrir la conexión
//            //        // Si la conexión se abre correctamente, notificar al usuario y devolver true
//            //        TaskDialog.Show("Conexión Exitosa", "Conexión a la base de datos establecida correctamente.");





//            //    }
//            //    catch (SqlException ex)
//            //    {
//            //        // Si ocurre algún error, mostrar un mensaje con la excepción y devolver false
//            //        TaskDialog.Show("Error de Conexión", "No se pudo conectar a la base de datos. Error: " + ex.Message);

//            //    }
//            //    finally
//            //    {
//            //        // Asegurarse de cerrar la conexión si está abierta
//            //        if (con.State == System.Data.ConnectionState.Open)
//            //        {
//            //            con.Close();
//            //        }
//            //    }
//            //}




//            //string nombreUsuario = txtNombreUsuario.Text;
//            //string email = txtEmail.Text;
//            //string password = txtPassword.Password;
//            //string confirmPassword = txtConfirmPassword.Password;

//            //// Validar que los campos no estén vacíos
//            //if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//            //{
//            //    MessageBox.Show("Por favor, rellene todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            //    return;
//            //}
//            //// Validar que las contraseñas coincidan
//            //if (password != confirmPassword)
//            //{
//            //    MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            //    return;
//            //}

//            //// Aquí puedes añadir la lógica para registrar al usuario en una base de datos o servicio
//            //MessageBox.Show($"Registro Exitoso\nUsuario: {nombreUsuario}\nEmail: {email}", "Registro");

//        }
//    }
//}
