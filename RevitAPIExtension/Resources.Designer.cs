﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RevitAPIExtension {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RevitAPIExtension.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using System.Threading.Tasks;
        ///using System.Reflection;
        ///using Autodesk.Revit.UI;
        ///using Autodesk.Revit;
        ///
        ///namespace RevitAPIExtension.Resources
        ///{
        ///    class AddinsPanel
        ///    {
        ///        public static void Build(UIControlledApplication application, string tabName)
        ///        {
        ///            string assemblyPath = Assembly.GetExecutingAssembly().Location;
        ///            
        ///			RibbonPanel panel = application.GetRibbonPanels()        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Addins_Panel {
            get {
                return ResourceManager.GetString("Addins_Panel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///#region $Unique_Name$
        ///
        ///PulldownButtonData $Var_Data$ = new PulldownButtonData(&quot;$Unique_Name$&quot;, &quot;$Text$&quot;);
        ///PulldownButton $Var_Button$ = panel.AddItem($Var_Data$) as PulldownButton;
        ///
        ///#endregion $Unique_Name$
        ///.
        /// </summary>
        internal static string PullDownButtonCode {
            get {
                return ResourceManager.GetString("PullDownButtonCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #region $Unique_Name$
        ///
        ///PushButtonData $Var_Data$ = new PushButtonData(&quot;$Unique_Name$&quot;, &quot;$Text$&quot;, assemblyPath, &quot;$Class_Name$&quot;);
        ///$AddItem$
        ///
        ///#endregion $Unique_Name$
        ///.
        /// </summary>
        internal static string PushButtonCode {
            get {
                return ResourceManager.GetString("PushButtonCode", resourceCulture);
            }
        }
    }
}
