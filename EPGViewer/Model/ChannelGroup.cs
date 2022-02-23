using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGViewer.Model
{
    class ChannelGroup
    {
        public string Name { get; set; }
        public ObservableCollection<ChannelItem> Channels { get; set; }
    }
}
