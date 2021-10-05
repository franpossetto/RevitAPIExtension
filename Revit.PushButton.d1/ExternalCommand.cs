using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace $safeprojectname$
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;

            try
            {
                //Check Revit Version
                if (!commandData.Application.Application.VersionName.Contains("2020"))
                {
                    using (TaskDialog taskDialog = new TaskDialog("Cannot Continue"))
                    {
                        taskDialog.TitleAutoPrefix = false;
                        taskDialog.MainInstruction = "Incompatible Version of Revit";
                        taskDialog.MainContent = "Main Content";
                        taskDialog.Show();
                    }
                    return Result.Cancelled;
                }

                TaskDialog.Show("From Revit", "Hello World");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
