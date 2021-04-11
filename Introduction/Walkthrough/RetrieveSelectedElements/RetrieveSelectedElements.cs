using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.Walkthrough 
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class RetrieveSelectedElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Autodesk.Revit.UI.Selection.Selection selection = uidoc.Selection;
                var collectionId = selection.GetElementIds();
                if (0 == collectionId.Count)
                {
                    TaskDialog.Show("Revit", "You haven't selected any elements.");
                }
                else
                {
                    string info = "Ids of selected elements in the document are: ";
                    foreach (var id in collectionId)
                    {
                        info += "\n\t" + id.IntegerValue;
                    }

                    TaskDialog.Show("Revit", info);
                }

            }
            catch (Exception e) 
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
