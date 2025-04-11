using Autodesk.Revit.DB.PointClouds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CANTIDADES.Views
{
    /// <summary>
    /// Lógica de interacción para ViewResistencia.xaml
    /// </summary>
    public partial class ViewResistencia : Window
    {
        public ViewResistencia()
        {
            InitializeComponent();
        }

        public string Resistecia { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string resistencia = ((ComboBoxItem)ResistenciasComboBox.SelectedItem).Content.ToString();

            Resistecia = resistencia;
            Close();
        }
        
    }
    
}
