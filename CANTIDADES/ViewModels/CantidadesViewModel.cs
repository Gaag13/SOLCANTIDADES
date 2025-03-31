using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CANTIDADES.Models;
using CANTIDADES.Utils;

namespace CANTIDADES.ViewModels
{
    public class CantidadesViewModel: ObservableObject
    {
        private UIDocument _uidoc;
        public List<Data> ContenidoTabla {  get; set; }
        public Dictionary<string,List<string>> TiposElementosNivel {  get; set; }

        private List<DosificacionConcreto> _dosificaciones;

        public CantidadesViewModel(UIDocument uidoc)
        {
            _uidoc = uidoc;
            CargarDatos();
            CargarDosificaciones();
        }
        private void CargarDosificaciones()
        {
            _dosificaciones = new List<DosificacionConcreto>
            {
                new DosificacionConcreto { Cantidades = "1 - 2 - 2", ResistenciaKgCm2 = 280, ResistenciaPSI = 4000, ResistenciaMpa = 27, Cemento = 420, Arena = 0.67, Grava = 0.67, Agua = 190 },
                new DosificacionConcreto { Cantidades = "1 - 2 - 2,5", ResistenciaKgCm2 = 240, ResistenciaPSI = 3555, ResistenciaMpa = 24, Cemento = 380, Arena = 0.60, Grava = 0.76, Agua = 180 },
                // Agregar el resto de las dosificaciones...
            };
        }

        public Dictionary<string, double> EstimarMateriales(double volumen, string resistencia)
        {
            var dosificacion = _dosificaciones.FirstOrDefault(d => d.ResistenciaKgCm2.ToString() == resistencia);
            if (dosificacion == null) return null;

            return new Dictionary<string, double>
            {
                { "Cemento", volumen * dosificacion.Cemento },
                { "Arena", volumen * dosificacion.Arena },
                { "Grava", volumen * dosificacion.Grava },
                { "Agua", volumen * dosificacion.Agua }
            };
        }
        public Dictionary<string, double> CalcularCantidadesMaterial()
        {
            var cantidadesTotales = new Dictionary<string, double>
            {
                { "Cemento", 0 },
                { "Arena", 0 },
                { "Grava", 0 },
                { "Agua", 0 }
            };

            foreach (var data in ContenidoTabla)
            {
                double volumen = double.Parse(data.VOLUMEN);
                var materiales = EstimarMateriales(volumen, "280"); // Ejemplo: resistencia de 280 kg/cm2

                cantidadesTotales["Cemento"] += materiales["Cemento"];
                cantidadesTotales["Arena"] += materiales["Arena"];
                cantidadesTotales["Grava"] += materiales["Grava"];
                cantidadesTotales["Agua"] += materiales["Agua"];
            }

            return cantidadesTotales;
        }
        private void CargarDatos()
        {
            var doc = _uidoc.Document;
            List<Element> list = RevitUtils.SelectElementMetrados(doc);
           
            var builtInParameterFloor = BuiltInParameter.SCHEDULE_LEVEL_PARAM;
            var builtInParameterWall = BuiltInParameter.WALL_BASE_CONSTRAINT;
            var builtInParameterColumn = BuiltInParameter.SCHEDULE_BASE_LEVEL_PARAM;
            var builtInParameterVigas = BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM;

#if REVIT2024_OR_GREATER

            List<string> nivelElementos = new List<string>();
            foreach (var element in list)
            {
                if ((BuiltInCategory)(int)element.Category.Id.Value == BuiltInCategory.OST_StructuralFoundation ||
                    (BuiltInCategory)(int)element.Category.Id.Value == BuiltInCategory.OST_Floors)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterFloor).AsValueString());
                }
                else if ((BuiltInCategory)(int)element.Category.Id.Value == BuiltInCategory.OST_Walls)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterWall).AsValueString());
                }
                else if ((BuiltInCategory)(int)element.Category.Id.Value== BuiltInCategory.OST_StructuralColumns)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterColumn).AsValueString());
                }
                else if ((BuiltInCategory)(int)element.Category.Id.Value == BuiltInCategory.OST_StructuralFraming)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterVigas).AsValueString());
                }
            }

            var nivelesUnicos = nivelElementos.Distinct().ToList();
            //RevitUtils.MostrarElementosStringEnTaskDialog(nivelElementos);
            TiposElementosNivel = new Dictionary<string, List<string>>();

            foreach (var nivel in nivelesUnicos)
            {
                List<string> existCategory = new List<string>();
                foreach (var elemento in list)
                {
                    if ((BuiltInCategory)(int)elemento.Category.Id.Value == BuiltInCategory.OST_StructuralFoundation ||
                        (BuiltInCategory)(int)elemento.Category.Id.Value == BuiltInCategory.OST_Floors)
                    {
                        if (elemento.get_Parameter(builtInParameterFloor).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)(int)elemento.Category.Id.Value == BuiltInCategory.OST_Walls)
                    {
                        if (elemento.get_Parameter(builtInParameterWall).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)(int)elemento.Category.Id.Value == BuiltInCategory.OST_StructuralColumns)
                    {
                        if (elemento.get_Parameter(builtInParameterColumn).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)(int)elemento.Category.Id.Value == BuiltInCategory.OST_StructuralFraming)
                    {
                        if (elemento.get_Parameter(builtInParameterVigas).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                }
                TiposElementosNivel.Add(nivel, existCategory.Distinct().ToList());
            }

#else
            List<string> nivelElementos = new List<string>();
            foreach (var element in list)
            {
                if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFoundation ||
                    (BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_Floors)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterFloor).AsValueString());
                }
                else if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_Walls)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterWall).AsValueString());
                }
                else if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterColumn).AsValueString());
                }
                else if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
                {
                    nivelElementos.Add(element.get_Parameter(builtInParameterVigas).AsValueString());
                }
            }

            var nivelesUnicos = nivelElementos.Distinct().ToList();
            TiposElementosNivel = new Dictionary<string, List<string>>();

            foreach (var nivel in nivelesUnicos)
            {
                List<string> existCategory = new List<string>();
                foreach (var elemento in list)
                {
                    if ((BuiltInCategory)elemento.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFoundation ||
                        (BuiltInCategory)elemento.Category.Id.IntegerValue == BuiltInCategory.OST_Floors)
                    {
                        if (elemento.get_Parameter(builtInParameterFloor).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)elemento.Category.Id.IntegerValue == BuiltInCategory.OST_Walls)
                    {
                        if (elemento.get_Parameter(builtInParameterWall).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)elemento.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns)
                    {
                        if (elemento.get_Parameter(builtInParameterColumn).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                    else if ((BuiltInCategory)elemento.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
                    {
                        if (elemento.get_Parameter(builtInParameterVigas).AsValueString() == nivel)
                        {
                            existCategory.Add(elemento.Category.Name);
                        }
                    }
                }
                TiposElementosNivel.Add(nivel, existCategory.Distinct().ToList());
            }
#endif
            ContenidoTabla = new List<Data>();
            for (int i = 0; i < nivelElementos.Count; i++)
            {
                ContenidoTabla.Add(new Data
                {
                    ID = list[i].Id.ToString(),
                    NIVEL = nivelElementos[i],
                    CATEGORIA = list[i].Category.Name,
                    AREA = Tools.Feet_to_m(list[i].get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble()).ToString("F3"),
                    VOLUMEN = Tools.Feet_to_m(list[i].get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble()).ToString("F3")
                });
                
            }
        }

#if !REVIT2024_OR_GREATER
        public void SeleccionarElementosRevit(List<string> ids)
        {
            ICollection<ElementId> coleccion = _uidoc.Selection.GetElementIds();
            coleccion.Clear();
            foreach (string id in ids)
            {
                ElementId idSeleccionado = new ElementId(int.Parse(id));
                coleccion.Add(idSeleccionado);
            }
            _uidoc.Selection.SetElementIds(coleccion);
        }

#else
        public void SeleccionarElementosRevit(List<string> ids)
        {
            ICollection<ElementId> coleccion = _uidoc.Selection.GetElementIds();
            coleccion.Clear();
            foreach (string id in ids)
            {
                ElementId idSeleccionado = new ElementId(long.Parse(id));
                coleccion.Add(idSeleccionado);
            }
            _uidoc.Selection.SetElementIds(coleccion);
        }

#endif
    }
}
