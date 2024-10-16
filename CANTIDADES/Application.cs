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
            var panel = Application.CreatePanel("Commands", "CANTIDADES");

            panel.AddPushButton<CmdKeyFireSharp>("Login")
                .SetImage("/QUANTITIES;component/Resources/Icons/CANT32X32.png")
                .SetLargeImage("/QUANTITIES;component/Resources/Icons/User16x16.png");

            panel.AddPushButton<CmdCantidades>("QUANTITIES")
                .SetAvailabilityController<AvailabilityButton>()
                .SetImage("/QUANTITIES;component/Resources/Icons/CANT32X32.png")
                .SetLargeImage("/QUANTITIES;component/Resources/Icons/CANT16X16.png");
        }
    }
}