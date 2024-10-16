using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANTIDADES.Utils
{
    public static class RevitUtils
    {
        public static List<Element> SelectElementMetrados(Document doc)
        {
            var wallCollector = new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToList();
            var floorCollector = new FilteredElementCollector(doc).OfClass(typeof(Floor)).ToList();
            var familyInstanceCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).ToList();
            List<Element> result = new List<Element>();
            result.AddRange(wallCollector);
            result.AddRange(floorCollector);
            result.AddRange(familyInstanceCollector);

            return result;
        }

        public static void MostrarElementosEnTaskDialog(List<Element> elementos)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Elementos seleccionados:");

            foreach (var element in elementos)
            {
                string elementName = element.Name;
                string elementId = element.Id.ToString();

                sb.AppendLine($"Elemento: {elementName}, ID: {elementId}");
            }
            TaskDialog.Show("Metrados - Elementos Seleccionados", sb.ToString());
        }
        public static void MostrarElementosStringEnTaskDialog(List<string> elementos)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Elementos seleccionados:");

            foreach (var element in elementos)
            {

                sb.AppendLine(element);
            }
            TaskDialog.Show("Elementos ", sb.ToString());
        }
    }
}
