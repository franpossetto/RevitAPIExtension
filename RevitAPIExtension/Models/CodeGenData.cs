using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension.Models
{
    public class CodeGenData
    {
        public string ClassName { get; set; }
        public string NameSpace { get; set; }
        public string Panel { get; set; }
        public ButtonReference Parent { get; set; }
        public ButtonType Type { get; set; }
        public string Text { get; set; }
    }
}
