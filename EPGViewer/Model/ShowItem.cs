using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGViewer.Model
{
    class ShowItem
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public DateTime StartTime { get; set; }
        public long StartTimestamp { get; set; }
        public DateTime EndTime { get; set; }
        public long EndTimestamp { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; } = true;
    }
}
