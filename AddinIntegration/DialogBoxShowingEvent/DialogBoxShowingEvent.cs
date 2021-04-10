using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

using RevitDialogEvents = Autodesk.Revit.UI.Events;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    public class DialogBoxShowingEvent : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            application.DialogBoxShowing += new EventHandler<RevitDialogEvents.DialogBoxShowingEventArgs>(AppDialogShowing);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            application.DialogBoxShowing -= new EventHandler<RevitDialogEvents.DialogBoxShowingEventArgs>(AppDialogShowing);
            return Result.Succeeded;
        }

        void AppDialogShowing(object sender, RevitDialogEvents.DialogBoxShowingEventArgs args)
        {
            int dialogId = args.HelpId;

            String promptInfo = "A Revit dialog will be opened.\n";
            promptInfo += "The help id of this dialog is " + dialogId.ToString() + "\n";
            promptInfo += "If you don't want the dialog to open, please press cancel button";

            TaskDialog taskDialog = new TaskDialog("Revit");
            taskDialog.MainContent = promptInfo;
            TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Ok |
                                     TaskDialogCommonButtons.Cancel;
            taskDialog.CommonButtons = buttons;
            TaskDialogResult result = taskDialog.Show();
            if (TaskDialogResult.Cancel == result)
            {
                args.OverrideResult(1);
            }
            else
            {
                args.OverrideResult(0);
            }
        }
    }
}