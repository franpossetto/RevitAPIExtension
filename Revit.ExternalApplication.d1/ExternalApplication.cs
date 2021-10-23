using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace $safeitemname$
{
    [Transaction(TransactionMode.Manual)]
    public class $safeitemname$ : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            return Result.Succeeded;
        }
    }
}


