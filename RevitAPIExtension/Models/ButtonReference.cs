using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension.Models
{
    public class ButtonReference
    {
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
