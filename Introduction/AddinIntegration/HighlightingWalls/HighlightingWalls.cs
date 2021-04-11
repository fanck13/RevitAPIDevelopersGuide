using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class HighlightingWalls : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData externalCommandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            message = "Please note the highlighted walls.";

            var collector = new FilteredElementCollector(externalCommandData.Application.ActiveUIDocument.Document);
            var collection = collector.OfClass(typeof(Wall)).ToElements();
            foreach (var e in collection)
            {
                elements.Insert(e);
            }

            return Result.Failed;
        }
    }
}
