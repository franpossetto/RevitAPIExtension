using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Outlining;
using RevitAPIExtension.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RevitAPIExtension.Commands
{
    static class CodeGen
    {
        private static string StartupClassName { get; set; }
        public static void Generate(CodeGenData data)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            var project = dte.Solution.Projects.Item(1);
            var projectItems = project.ProjectItems;
            ProjectItem addin = GetAddin(projectItems);
            string className = GetStartupClassName(addin);
            if (className is null)
                return;
            var startup = GetStartupItem(projectItems, className);
            Initialize(startup, project);
            InsertCode(projectItems, data);
        }
        private static void Initialize(ProjectItem startup, Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (startup != null)
            {
                CreateFile(project, out bool isNew);
                if (isNew)
                {
                    var range = GetOnStartupRange(startup);
                    SetRunCode(startup, range);
                }
            }
        }
        private static void InsertCode(ProjectItems items, CodeGenData data)
        {   
            ThreadHelper.ThrowIfNotOnUIThread();
            var panelItem = items.Find(item => item.Name == "Addins.Panel.cs");
            if (panelItem is null)
                return;
            var windowItem = panelItem.Open();
            var selection = panelItem.Document.Selection as TextSelection;
            selection.SelectAll();
            var regex = new Regex(@"#region(?: |\t)*(?<name>.*)$(?<content>(.|\n)*?)#endregion(?: |\t)*\k<name>", RegexOptions.Multiline);
            var matches = regex.Matches(selection.Text);
            var regions = new List<RegionData>();
            foreach (Match match in matches)
            {
                regions.Add(new RegionData(match.Groups["name"], match.Groups["content"], match.Index, match.Length));
            }
            switch (data.Type)
            {
                case ButtonType.Push:
                    SetPushButton(regions.SingleOrDefault(r => r.Name.Value.StartsWith("PushButtons")), data, selection);
                    break;
                case ButtonType.Stack:
                    SetPushButton(regions.SingleOrDefault(r => r.Name.Value.StartsWith("PushButtons")), data, selection);
                    break;
                case ButtonType.PullDown:
                    SetPullDownButton(regions.SingleOrDefault(r => r.Name.Value.StartsWith("PullDownButtons")), data, selection);
                    break;
                default:
                    break;
            }
            panelItem.Document.Save();
            windowItem.Close();
        }
        private static void SetPushButton(RegionData region, CodeGenData data, TextSelection selection)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            int i = region.Content.Index;
            string uniqueName = data.NameSpace.Replace(".", "_") + "_" + data.ClassName;
            RemoveIfExist(region, selection, uniqueName);
            string textFirst = selection.Text.Substring(0, i + 1); //+1 para tomar el \n del inicio
            var code = GetPushButtonCode(data, uniqueName);
            string textLast = selection.Text.Substring(i + 1);//+1 porque se tomo anteriormente
            selection.Insert(textFirst + code + textLast);
            selection.SelectAll();
            selection.SmartFormat();
        }
        private static string GetPushButtonCode(CodeGenData data, string uniqueName)
        {
            string template = Resources.PushButtonCode;
            string code = template.Replace("$Unique_Name$", uniqueName);
            code = code.Replace("$Text$", data.Text);
            code = code.Replace("$Class_Name$", data.ClassName);
            string varName = uniqueName + "_" + "Button";
            string varData = uniqueName + "_" + "Data";
            code = code.Replace("$Var_Data$", (uniqueName + "_" + "Data"));
            if (data.Parent is null)
            {
                code = code.Replace("$AddItem$", $"PushButton {varName} = panel.AddItem({varData}) as PushButton;");
            }
            else
            {
                string parentName = data.Parent.UniqueName.Substring(0, data.Parent.UniqueName.Length - 1);
                string varParent = parentName + "_" + "Button";
                code = code.Replace("$AddItem$", $"{varParent}.AddPushButton({varData});");
            }
            return code;
        }
        private static void SetPullDownButton(RegionData region, CodeGenData data, TextSelection selection)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            int i = region.Content.Index;
            string uniqueName = data.NameSpace.Replace(".", "_") + "_" + data.ClassName;
            RemoveIfExist(region, selection, uniqueName);
            string textFirst = selection.Text.Substring(0, i + 1); //+1 para tomar el \n del inicio
            var code = GetPullDownButtonCode(data, uniqueName);
            string textLast = selection.Text.Substring(i + 1);//+1 porque se tomo anteriormente
            selection.Insert(textFirst + code + textLast);
            selection.SelectAll();
            selection.SmartFormat();
        }
        private static string GetPullDownButtonCode(CodeGenData data, string uniqueName)
        {
            string template = Resources.PullDownButtonCode;
            string code = template.Replace("$Unique_Name$", uniqueName);
            code = code.Replace("$Text$", data.Text);
            code = code.Replace("$Var_Data$", uniqueName + "_" + "Data");
            code = code.Replace("$Var_Button$", uniqueName + "_" + "Button");
            return code;
        }
        private static void RemoveIfExist(RegionData region, TextSelection selection, string name)
        {
            var regex = new Regex(@"#region(?: |\t)*(?<name>.*)$(?<content>(.|\n)*?)#endregion(?: |\t)*\k<name>", RegexOptions.Multiline);
            var subRegions = new List<RegionData>();
            foreach (Match match in regex.Matches(region.Content.Value))
            {
                subRegions.Add(new RegionData(match.Groups["name"], match.Groups["content"], match.Index, match.Length));
            }
            var oldRegion = subRegions.SingleOrDefault(r => r.Name.Value.StartsWith(name));
            if (oldRegion != null)
            {
                int i = region.Content.Index + oldRegion.Index;
                int x = i + oldRegion.Length;
                string textFirst = selection.Text.Substring(0, i);
                string textLast = selection.Text.Substring(x + 1);
                selection.Insert(textFirst + textLast);
                selection.SelectAll();
            }
        }
        private static ProjectItem GetAddin(ProjectItems items)
        {
            return items.Find(item =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                var extension = item.Name.Split('.').Last();
                return extension == "addin";
            });
        }
        private static void CreateFile(Project project, out bool isNew)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var items = project.ProjectItems;
            bool exists = items.Exist(item => item.Name == "Addins.Panel.cs");
            if (!exists)
            {
                string fileName = Directory.GetCurrentDirectory() + @"\Resources\Addins.Panel.cs";
                if (!File.Exists(fileName))
                {
                    using (var file = File.Create(fileName))
                    {
                        var content = new UTF8Encoding(true).GetBytes(Resources.Addins_Panel);
                        file.Write(content, 0, content.Length);
                        file.Close();
                    }
                }
                items.AddFromFileCopy(fileName);//Agregar archivo
                isNew = true;
            }
            else
            {
                isNew = false;
            }
        }
        private static string GetStartupClassName(ProjectItem addin)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if(StartupClassName != null)
            {
                return StartupClassName;
            }
            if (addin == null)
                return null;
            var windowAddin = addin.Open();
            var addinSelection = addin.Document.Selection as TextSelection;
            addinSelection.SelectAll();
            var addinXml = new XmlDocument();
            addinXml.LoadXml(addinSelection.Text);
            var fullClassName = addinXml.SelectSingleNode("RevitAddIns/AddIn/FullClassName").InnerText;
            windowAddin.Close();
            StartupClassName = fullClassName.Split('.').Last();
            return StartupClassName;
        }
        private static ProjectItem GetStartupItem(ProjectItems items, string className)
        {
            return items.Find(i => i.Name == className + ".cs") ?? FindStartupItem(items, className);
        }
        private static ProjectItem FindStartupItem(ProjectItems items, string className)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string pattern = $@"class\s*{className}\s*:\s*IExternalApplication";
            return items.Find(item =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                var extension = item.Name.Split('.').Last();
                if (extension == "cs")
                {
                    var itemWindow = item.Open();
                    var selectionItem = item.Document.Selection as TextSelection;
                    selectionItem.SelectAll();
                    if (Regex.IsMatch(selectionItem.Text, pattern))
                    {
                        itemWindow.Close();
                        return true;
                    }
                }
                return false;
            });
        }
        private static RangeString GetOnStartupRange(ProjectItem startup)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var startupWindow = startup.Open();
            var selectionStartup = startup.Document.Selection as TextSelection;
            selectionStartup.SelectAll();
            string patern = @"OnStartup\(UIControlledApplication\s*application\)\s*({(.|\n)*?})";
            var regex = new Regex(patern);
            var match = regex.Match(selectionStartup.Text);
            var group = match.Groups[1];
            startupWindow.Close();
            return new RangeString(group.Index, group.Index + group.Length);
        }
        private static void SetRunCode(ProjectItem startup, RangeString range)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Window startupWindow = startup.Open();
            var selectionStartup = startup.Document.Selection as TextSelection;
            selectionStartup.SelectAll();
            var regex = new Regex(@"\s*return\s*Result\.Succeeded;");
            var maches = regex.Matches(selectionStartup.Text);
            foreach (Match mach in maches)
            {
                if (mach.Index > range.StartIndex && mach.Index < range.EndIndex)
                {
                    string _using = "using RevitAPIExtension.Resources;\r\n";
                    string first = selectionStartup.Text.Substring(0, mach.Index + 1);
                    string code = "\r\nAddinsPanel.Build(application, null);\r\n";
                    string last = selectionStartup.Text.Substring(mach.Index + 1);
                    selectionStartup.Insert(_using + first + code + last);
                    selectionStartup.SelectAll();
                    selectionStartup.SmartFormat();
                }
            }
            startup.Document.Save();
            startupWindow.Close();
        }
        public static UIDefaultData GetDefaultData()
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
                var selection = dte.ActiveDocument.Selection as TextSelection;
                selection.SelectAll();
                var project = dte.Solution.Projects.Item(1);
                var projectItems = project.ProjectItems;
                ProjectItem addin = GetAddin(projectItems);
                string className = GetStartupClassName(addin);
                if (className is null)
                    return null;
                var startup = GetStartupItem(projectItems, className);
                Initialize(startup, project);
                //var panelsItemList = projectItems.FindAll(i => i.Name.EndsWith(".panel.cs"));
                var panelItem = projectItems.Find(item => item.Name == "Addins.Panel.cs");
                var panelFiles = GetPanelData(new List<ProjectItem>() { panelItem });
                var commandRegex = new Regex(@"namespace\s*(?'nameSpace'(?:\w|\.)+)\s*{(?:.|\n)*?^\s*\w*\s*class\s*(?'className'\w+)\s*:\s*IExternalCommand", RegexOptions.Multiline);
                var match = commandRegex.Match(selection.Text);
                return new UIDefaultData()
                {
                    Panels = panelFiles,
                    Default = new CodeGenData()
                    {
                        NameSpace = match.Groups["nameSpace"].Value,
                        ClassName = match.Groups["className"].Value,
                        Text = match.Groups["className"].Value,
                        Type = ButtonType.Push,
                        Panel = "Addins"
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static List<RegionData> GetButtonsRegion(RegionData parent)
        {
            var regionRegex = new Regex(@"#region(?: |\t)*(?<name>.*)$(?<content>(.|\n)*?)#endregion(?: |\t)*\k<name>", RegexOptions.Multiline);
            var matches = regionRegex.Matches(parent.Content.Value);
            var regions = new List<RegionData>();
            foreach (Match match in matches)
            {
                regions.Add(new RegionData(match.Groups["name"], match.Groups["content"], match.Index, match.Length));
            }
            return regions;
        }
        private static List<PanelFile> GetPanelData(List<ProjectItem> panels)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var result = new List<PanelFile>();
            var regionRegex = new Regex(@"#region(?: |\t)*(?<name>.*)$(?<content>(.|\n)*?)#endregion(?: |\t)*\k<name>", RegexOptions.Multiline);
            foreach (ProjectItem panel in panels)
            {
                var windowPanel = panel.Open();
                var selection = panel.Document.Selection as TextSelection;
                selection.SelectAll();
                var matches = regionRegex.Matches(selection.Text);
                RegionData pulldownRegion = null;
                foreach (Match match in matches)
                {
                    if (match.Groups["name"].Value.StartsWith("PullDownButtons"))
                        pulldownRegion = new RegionData(match.Groups["name"], match.Groups["content"], match.Index, match.Length);
                }
                if (pulldownRegion is null)
                    throw new Exception($"No se encuentra la region PullDowButons en el archivo de panel {panel.Name}");
                else
                {
                    result.Add(new PanelFile()
                    {
                        Name = panel.Name.Split('.')[0],
                        FullName = panel.FileNames[0],
                        ParentRegion = pulldownRegion,
                        PullDowns = GetButtonsRegion(pulldownRegion)
                    });
                }
                windowPanel.Close(vsSaveChanges.vsSaveChangesNo);
            }
            return result;
        }
    }
}
