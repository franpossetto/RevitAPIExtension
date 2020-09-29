using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Creation;
using Autodesk.Revit.UI;

namespace Revit_Commands
{
    public class RibbonBundle
    {
        public static void Run(UIControlledApplication application)
        {
            // Create elements inside the Regions.

            //Code inside regions was read from template files.

            #region Revit Tabs
            
            /// <summary>
            /// Create a new Tab on Ribbon Bar.
            /// </summary>

            const string RIBBON_TAB = "Revit API Extension";
            Ribbon.CreateRibbonTab(application, RIBBON_TAB);

            #endregion

            #region Revit Panels
            
            /// <summary>
            /// Create a new Panel on Ribbon Tab.
            /// </summary>

            const string RIBBON_PANEL = "My Addins";
            RibbonPanel ribbonPanel = Ribbon.CreateRibbonPanel(application, RIBBON_PANEL, RIBBON_TAB);
            
            #endregion

            #region Revit Push Buttons
            
            /// <summary>
            /// Create new Buttons on Panel.
            /// </summary>
            
            // Attributs
            const string PUSH_BUTTON_NAME = "Revit Push Button";
            const string PUSH_BUTTON_TEXT = "Revit Plugin";
            System.Drawing.Bitmap ico = Properties.Resources.icon;
            System.Windows.Media.Imaging.BitmapSource icon30 = Ribbon.Icon(ico);
            
            PushButtonData pushDataButton = Ribbon.CreatePushButtonData(PUSH_BUTTON_NAME, PUSH_BUTTON_TEXT, "Revit_Commands.Cmd1");
            
            //Attributes
            pushDataButton.LongDescription = "Long description of the command tooltip";
            pushDataButton.ToolTip = "The description that appears as a ToolTip for the item";

            PushButton pushButton = ribbonPanel.AddItem(pushDataButton) as PushButton;
            pushButton.LargeImage = icon30;

            #endregion
        }

    }
}
