using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class ManifestHandler : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var manifest = new Autodesk.RevitAddIns.RevitAddInManifest();
            var command1 = new Autodesk.RevitAddIns.RevitAddInCommand(@"full path\assemblyName.dll", Guid.NewGuid(), 
                "RevitAPIDevelopersGuide.AddinIntegration.ManifestHandler", "");
            command1.Description = "description";
            command1.Text = "display text";

            command1.Discipline = Autodesk.RevitAddIns.Discipline.Mechanical 
                | Autodesk.RevitAddIns.Discipline.Electrical 
                | Autodesk.RevitAddIns.Discipline.Piping 
                | Autodesk.RevitAddIns.Discipline.Structure;

            command1.VisibilityMode = Autodesk.RevitAddIns.VisibilityMode.NotVisibleInFamily;

            Autodesk.RevitAddIns.RevitAddInApplication application1 = new Autodesk.RevitAddIns.RevitAddInApplication("appName",
                "full path\\assemblyName.dll", Guid.NewGuid(), "namespace.className", "");

            manifest.AddInCommands.Add(command1);
            manifest.AddInApplications.Add(application1);

            var revitProduct1 = Autodesk.RevitAddIns.RevitProductUtility.GetAllInstalledRevitProducts()[0];
            manifest.SaveAs(revitProduct1.AllUsersAddInFolder + "\\RevitAddInUtilitySample.addin");

            var revitProduct2 = Autodesk.RevitAddIns.RevitProductUtility.GetAllInstalledRevitProducts()[0];

            var revitAddInManifest =
                 Autodesk.RevitAddIns.AddInManifestUtility.GetRevitAddInManifest(
                      revitProduct2.AllUsersAddInFolder + "\\RevitAddInUtilitySample.addin");

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
