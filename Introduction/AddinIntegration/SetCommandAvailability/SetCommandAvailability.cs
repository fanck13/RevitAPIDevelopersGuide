using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    public class SetCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(Autodesk.Revit.UI.UIApplication applicationData, CategorySet selectedCategories)
        {
            if (selectedCategories.IsEmpty)
                return true;

            foreach (Category c in selectedCategories)
            {
                if (c.Id.IntegerValue == (int)BuiltInCategory.OST_Walls)
                    return true;
            }
            return false;
        }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class CommandAvailabilityTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}