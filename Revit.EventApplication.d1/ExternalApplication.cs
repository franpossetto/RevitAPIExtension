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
    public class ExternalApplication : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            application.ControlledApplication.DocumentOpened -= application_DocumentOpened;
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                // Register event. 
                application.ControlledApplication.DocumentOpened += new EventHandler<Autodesk.Revit.DB.Events.DocumentOpenedEventArgs>(application_DocumentOpened);
            }

            catch (Exception)
            {
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        public void application_DocumentOpened(object sender, DocumentOpenedEventArgs args)
        {
            // get document from event args.
            Document doc = args.Document;
            TaskDialog.Show("Hello", "Document is opened, now do something");
        }
    }
}



   
