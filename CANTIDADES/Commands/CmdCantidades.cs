﻿using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using CANTIDADES.ViewModels;
using CANTIDADES.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANTIDADES.Commands
{
    /// <summary>
    /// Punto de entrada del comando externo invocado desde la interfaz de revit.
    /// </summary>

    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CmdCantidades: ExternalCommand
    {
        public override void Execute()
        {
            var uidoc = UiDocument;            
            var viewModel = new CantidadesViewModel(uidoc);
            var view = new ViewCantidades(viewModel);
            view.ShowDialog();
        }
    }
}
