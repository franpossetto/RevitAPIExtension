using System.Collections.Generic;
using System.IO;
using System;

namespace RevitAPIExtension.Wizards
{
    public class Path
    {
        private Dictionary<string, string> _packageVersion;
        private Dictionary<string, string> _revitDllPath;
        private Dictionary<string, string> _referencePath;
        public string baseRefDir = "\\.nuget\\packages\\revit_all_main_versions_api_x64";

        public Dictionary<string, string> PackageVersion
        {
            set { _packageVersion = value;  }
            get { return _packageVersion; }
        }

        public Dictionary<string, string> ReferencePath
        {
            set { _referencePath = value; }
            get { return _referencePath; }
        }

        public Dictionary<string, string> RevitDllPath
        {
            set { _revitDllPath = value; }
            get { return _revitDllPath; }
        }

        public Path()
        {
            Dictionary<string, string> aux = new Dictionary<string, string>();
            aux.Add("2017", "2017.0.2");
            aux.Add("2018", "2018.0.2");
            aux.Add("2019", "2019.0.1");
            aux.Add("2020", "2020.0.1");
            aux.Add("2021", "2021.1.4");

            _packageVersion = aux;

            Dictionary<string, string> aux2 = new Dictionary<string, string>();
            aux2.Add("2017", "\\2017.0.2\\lib\\net46");
            aux2.Add("2018", "\\2018.0.2\\lib\\net46");
            aux2.Add("2019", "\\2019.0.1\\lib\\net47");
            aux2.Add("2020", "\\2020.0.1\\lib\\net47");
            aux2.Add("2021", "\\2021.1.4\\lib\\net48");

            _referencePath = aux2;

            Dictionary<string, string> aux3 = new Dictionary<string, string>();
            string basicfolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            aux3.Add("2017", basicfolder + "\\Autodesk\\Revit 2017\\RevitAPI.dll");
            aux3.Add("2018", basicfolder + "\\Autodesk\\Revit 2018\\RevitAPI.dll");
            aux3.Add("2019", basicfolder + "\\Autodesk\\Revit 2019\\RevitAPI.dll");
            aux3.Add("2020", basicfolder + "\\Autodesk\\Revit 2020\\RevitAPI.dll");
            aux3.Add("2021", basicfolder + "\\Autodesk\\Revit 2021\\RevitAPI.dll");

            _revitDllPath = aux3;

        }

        public List<string> unavalaibleVersions()
        {
            List<string> versions = new List<string>();

            foreach (KeyValuePair<string, string> path in RevitDllPath)
            {
                if (!File.Exists(path.Value))
                {
                    versions.Add(path.Key);
                }
            }

            return versions;
        }
    }
}
