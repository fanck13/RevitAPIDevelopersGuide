using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    public class DisplayRevitStyleTaskDialog : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
            Document activeDoc = commandData.Application.ActiveUIDocument.Document;

            TaskDialog mainDialog = new TaskDialog("Hello, Revit!");
            mainDialog.MainInstruction = "Hello, Revit!";
            mainDialog.MainContent =
                    "This sample shows how to use a Revit task dialog to communicate with the user."
                    + "The command links below open additional task dialogs with more information.";

            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1,
                     "View information about the Revit installation");
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2,
                            "View information about the active document");

            mainDialog.CommonButtons = TaskDialogCommonButtons.Close;
            mainDialog.DefaultButton = TaskDialogResult.Close;

            mainDialog.FooterText =
                    "" + "Click here for the Revit API Developer Center";

            TaskDialogResult tResult = mainDialog.Show();

            if (TaskDialogResult.CommandLink1 == tResult)
            {
                TaskDialog dialog_CommandLink1 = new TaskDialog("Revit Build Information");
                dialog_CommandLink1.MainInstruction =
                        "Revit Version Name is: " + app.VersionName + "\n"
                 + "Revit Version Number is: " + app.VersionNumber + "\n"
                        + "Revit Version Build is: " + app.VersionBuild;

                dialog_CommandLink1.Show();

            }
            else if (TaskDialogResult.CommandLink2 == tResult)
            {
                TaskDialog.Show("Active Document Information",
                        "Active document: " + activeDoc.Title + "\n"
                 + "Active view name: " + activeDoc.ActiveView.Name);
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
