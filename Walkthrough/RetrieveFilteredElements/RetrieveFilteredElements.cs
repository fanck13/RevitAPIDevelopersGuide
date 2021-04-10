using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.Walkthrough
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class RetrieveFilteredElements
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                ElementClassFilter familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));
                ElementCategoryFilter doorsCategoryfilter =
                        new ElementCategoryFilter(BuiltInCategory.OST_Doors);
                LogicalAndFilter doorInstancesFilter =
                        new LogicalAndFilter(familyInstanceFilter, doorsCategoryfilter);
                FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);
                var doors = collector.WherePasses(doorInstancesFilter).ToElementIds();

                if (0 == doors.Count)
                {
                    TaskDialog.Show("Revit", "You haven't selected any elements.");
                }
                else
                {
                    string info = "The ids of the doors in the current document are:";
                    foreach (var id in doors)
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
