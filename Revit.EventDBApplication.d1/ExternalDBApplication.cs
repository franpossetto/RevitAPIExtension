using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace $safeprojectname$
{
    [Transaction(TransactionMode.Manual)]
    public class ExternalDBApplication : IExternalDBApplication
    {
        ExternalDBApplicationResult IExternalDBApplication.OnShutdown(ControlledApplication application)
        {
            application.DocumentOpened -= application_DocumentOpened;
            return Autodesk.Revit.DB.ExternalDBApplicationResult.Succeeded;
        }

        ExternalDBApplicationResult IExternalDBApplication.OnStartup(ControlledApplication application)
        {
            try
            {
                // Register event. 
                application.DocumentOpened += new EventHandler<Autodesk.Revit.DB.Events.DocumentOpenedEventArgs>(application_DocumentOpened);
            }

            catch (Exception)
            {
                return Autodesk.Revit.DB.ExternalDBApplicationResult.Failed;
            }

            return Autodesk.Revit.DB.ExternalDBApplicationResult.Succeeded;
        }

        public void application_DocumentOpened(object sender, DocumentOpenedEventArgs args)
        {
            // get document from event args.
            Document doc = args.Document;
            TaskDialog.Show("Hello", "Document is opened, now do something");
        }
    }

}


