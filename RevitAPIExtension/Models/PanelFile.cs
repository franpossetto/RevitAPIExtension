using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension.Models
{
    public class PanelFile
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public List<RegionData> PullDowns { get; set; }
        public RegionData ParentRegion { get; set; }
    }
}
