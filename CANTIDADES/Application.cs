using CANTIDADES.Commands;
using CANTIDADES.Models;
using Nice3point.Revit.Toolkit.External;

namespace CANTIDADES
{
    /// <summary>
    ///     Application entry point
    /// </summary>
    [UsedImplicitly]
    public class Application : ExternalApplication
    {
        public override void OnStartup()
        {
            CreateRibbon();
        }

        private void CreateRibbon()
        {
            var panel = Application.CreatePanel("Cuantificacióm", "CANTIDADES");

            panel.AddPushButton<CmdKeyFireSharp>("Login")
                .SetLargeImage("/CANTIDADES;component/Resources/Icons/User16x16.png");

            panel.AddPushButton<CmdCantidades>("QUANTITIES")
                .SetAvailabilityController<AvailabilityButton>()
                .SetLargeImage("/CANTIDADES;component/Resources/Icons/Contar16x16.png");
        }
    }
}