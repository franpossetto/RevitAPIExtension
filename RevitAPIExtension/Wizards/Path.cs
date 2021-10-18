using System.Collections.Generic;

namespace RevitAPIExtension.Wizards
{
    public class Path
    {
        private Dictionary<string, string> _packageVersion;
        private Dictionary<string, string> _revitcopyPath;
        private Dictionary<string, string> _referencePath;
        public string baseRefDir = "\\.nuget\\packages\\Revit_All_Main_Versions_API_x64";

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
        }

    }
}
