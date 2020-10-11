using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Outlining;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace RevitAPIExtension.QuickActions.Ribbon
{
    static class CodeGen
    {
        private static string StartupClassName { get; set; }
        public static void Generate(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            var project = dte.Solution.Projects.Item(1);
            var projectItems = project.ProjectItems;
            ProjectItem addin = GetAddin(projectItems);
            string className = GetStartupClassName(addin);
            if(className is null)
                return;
            var startup = GetStartupItem(projectItems, className);
            Initialize(startup, project);
            InsertCode(projectItems, name);
        }
        private static void Initialize(ProjectItem startup, Project project)
        {
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
        private static void InsertCode(ProjectItems items, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var panelItem = items.Find(item => item.Name == "AddinsPanel.cs");
            if (panelItem is null)
                return;
            var windowItem = panelItem.Open();
            var selection = panelItem.Document.Selection as TextSelection;
            selection.SelectAll();
            var regex = new Regex(@"#region(?: |\t)*(?<name>.*)$(?<content>(.|\n)*?)#endregion(?: |\t)*\k<name>", RegexOptions.Multiline);
            var matches = regex.Matches(selection.Text);
            var regions = new List<RegionData>();
            foreach (Match match in regex.Matches(selection.Text))
            {
                regions.Add(new RegionData(match.Groups["name"], match.Groups["content"], match.Index, match.Length));
            }
            SetPushButton(regions, name, selection);
            panelItem.Document.Save();
            windowItem.Close();
        }
        private static void SetPushButton(List<RegionData> regions, string name, TextSelection selection)
        {
            var pushButtonRegion = regions.Single(r => r.Name.Value.StartsWith("PushButtons"));
            int i = pushButtonRegion.Content.Index;
            RemoveIfExist(pushButtonRegion, selection, name);
            string textFirst = selection.Text.Substring(0, i + 1); //++1 para tomar el \n del inicio
            var code = GetPushButtonCode(name);
            string textLast = selection.Text.Substring(i +1);
            selection.Insert(textFirst + code + textLast);
            selection.SelectAll();
            selection.SmartFormat();
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
        private static string GetPushButtonCode(string name)
        {
            string template = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Resources\PushButtonCode.txt");
            string code = template.Replace("$Unique_Name$", name);
            code = code.Replace("$Label$", name);
            code = code.Replace("$Class_Name$", name);
            code = code.Replace("$Var_Data$", name + "Data");
            code = code.Replace("$Var_Button$", name + "Button");
            return code;
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
            bool exists = items.Exist(item => item.Name == "AddinsPanel.cs");
            if (!exists)
            {
                items.AddFromFileCopy(Directory.GetCurrentDirectory() + @"\Resources\AddinsPanel.cs");//Agregar archivo
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
            var startupWindow = startup.Open();
            var selectionStartup = startup.Document.Selection as TextSelection;
            selectionStartup.SelectAll();
            string patern = @"\s*return\s*Result\.Succeeded;";
            var regex = new Regex(patern);
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
    }
    class RangeString
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public RangeString(int start, int end)
        {
            this.StartIndex = start;
            this.EndIndex = end;
        }

    }
    class RegionData
    {
        public Group Name { get; set; }
        public Group Content { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
        public RegionData(Group name, Group content, int index, int length)
        {
            Name = name;
            Content = content;
            Index = index;
            Length = length;
        }
    }
}
