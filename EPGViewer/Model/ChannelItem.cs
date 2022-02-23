using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGViewer.Model
{
    class ChannelItem
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ChannelItem(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
