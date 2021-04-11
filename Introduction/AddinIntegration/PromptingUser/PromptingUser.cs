using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class PromptingUser : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData externalCommandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            try
            {
                var uidoc = externalCommandData.Application.ActiveUIDocument;
                var doc = uidoc.Document;

                var ids = doc.Delete(uidoc.Selection.GetElementIds());
                var taskDialog = new TaskDialog("Revit");

                taskDialog.MainContent = ("Click Yes to return Succeeded. Selected members will be deleted.\n" +
                        "Click No to return Failed.  Selected members will not be deleted.\n" +
                        "Click Cancel to return Cancelled.  Selected members will not be deleted.");

                var buttons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No | TaskDialogCommonButtons.Cancel;
                taskDialog.CommonButtons = buttons;

                var taskDialogResult =  taskDialog.Show();

                if (taskDialogResult == TaskDialogResult.Yes)
                {
                    return Autodesk.Revit.UI.Result.Succeeded;
                }
                else if (taskDialogResult == TaskDialogResult.No)
                {
                    var elementIds = uidoc.Selection.GetElementIds();
                    foreach (var id in elementIds)
                    {
                        elements.Insert(doc.GetElement(id));
                    }
                    message = "Failed to delete selection.";
                    return Autodesk.Revit.UI.Result.Failed;
                }
                else
                {
                    return Autodesk.Revit.UI.Result.Cancelled;
                }

            }
            catch
            {
                message = "Unexpected Exception thrown.";
                return Autodesk.Revit.UI.Result.Failed;
            }
        }
    }
}
