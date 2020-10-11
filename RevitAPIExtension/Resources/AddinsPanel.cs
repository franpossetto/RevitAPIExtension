﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.Revit.UI;
using Autodesk.Revit;

namespace RevitAPIExtension.Resources
{
    class AddinsPanel
    {
        public static void Build(UIControlledApplication application, string tabName)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            RibbonPanel panel = application.GetRibbonPanels()
	            .FirstOrDefault(p => p.Name.Equals("addinPanel", StringComparison.InvariantCulture));
            panel = panel ?? application.CreateRibbonPanel("addinPanel");
            #region PushButtons
            #endregion PushButtons
        }
    }
}
