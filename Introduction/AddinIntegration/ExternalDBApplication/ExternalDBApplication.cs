using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;

namespace RevitAPIDevelopersGuide.AddinIntegration
{
    public class ExternalDBApplication : IExternalDBApplication
    {
        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            throw new NotImplementedException();
        }

        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            throw new NotImplementedException();
        }
    }
}
