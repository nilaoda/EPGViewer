using EPGViewer.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace EPGViewer.ViewModel
{
    class ComboBoxViewModel
    {
        private ObservableCollection<ChannelGroup> channelTypeList;
        public ObservableCollection<ChannelGroup> ChannelTypeList
        {
            get => Channels;
            set => channelTypeList = value;
        }

        #region 所有台
        static readonly ObservableCollection<ChannelGroup> Channels = new ObservableCollection<ChannelGroup>()
        {
            new ChannelGroup()
            {
                Name = "央视频道",
                Channels = new ObservableCollection<ChannelItem>()
                {
                    new ChannelItem("CCTV-1", "cctv1"),
                    new ChannelItem("CCTV-2", "cctv2"),
                    new ChannelItem("CCTV-3", "cctv3"),
                    new ChannelItem("CCTV-4", "cctv4"),
                    new ChannelItem("CCTV-4 美洲", "cctvamerica"),
                    new ChannelItem("CCTV-4 欧洲", "cctveurope"),
                    new ChannelItem("CCTV-5", "cctv5"),
                    new ChannelItem("CCTV-5+", "cctv5plus"),
                    new ChannelItem("CCTV-6", "cctv6"),
                    new ChannelItem("CCTV-7", "cctv7"),
                    new ChannelItem("CCTV-8", "cctv8"),
                    new ChannelItem("CCTV-9", "cctv9"),
                    new ChannelItem("CCTV-10", "cctv10"),
                    new ChannelItem("CCTV-11", "cctv11"),
                    new ChannelItem("CCTV-12", "cctv12"),
                    new ChannelItem("CCTV-13", "cctv13"),
                    new ChannelItem("CCTV-14", "cctv14"),
                    new ChannelItem("CCTV-15", "cctv15"),
                    new ChannelItem("CCTV-16", "cctv16"),
                    new ChannelItem("CCTV-17", "cctv17"),
                    new ChannelItem("CCTV-4K", "cctv4k"),
                    new ChannelItem("CCTV-8K", "cctv8k"),
                    new ChannelItem("CCTV第一剧场", "diyijuchang"),
                    new ChannelItem("CCTV风云剧场", "fyjc"),
                    new ChannelItem("CCTV风云音乐", "fyyy"),
                    new ChannelItem("CCTV风云足球", "cctvfyzq"),
                    new ChannelItem("CCTV高尔夫网球", "cctvgaowang"),
                    new ChannelItem("CCTV国防军事", "guofang"),
                    new ChannelItem("CCTV怀旧剧场", "hjjc"),
                    new ChannelItem("CCTV老故事", "cctvlaogushi"),
                    new ChannelItem("CCTV女性时尚", "shishang"),
                    new ChannelItem("CCTV气象", "cctvqixiang"),
                    new ChannelItem("CCTV世界地理", "shijiedili"),
                    new ChannelItem("CCTV新科动漫", "xinkedongman"),
                    new ChannelItem("CCTV戏曲", "cctvxiqu"),
                    new ChannelItem("CCTV央视台球", "taiqiu"),
                    new ChannelItem("CCTV央视文化精品", "jingpin"),
                    new ChannelItem("CCTV娱乐", "cctvyule"),
                    new ChannelItem("CCTV中视购物", "dianshigouwu")
                }
            },
            new ChannelGroup()
            {
                Name = "卫视频道",
                Channels = new ObservableCollection<ChannelItem>()
                {
                    new ChannelItem("安徽卫视", "anhui"),
                    new ChannelItem("北京卫视", "btv1"),
                    new ChannelItem("重庆卫视", "chongqing"),
                    new ChannelItem("东方卫视", "dongfang"),
                    new ChannelItem("东南卫视", "dongnan"),
                    new ChannelItem("甘肃卫视", "gansu"),
                    new ChannelItem("广东卫视", "guangdong"),
                    new ChannelItem("广西卫视", "guangxi"),
                    new ChannelItem("贵州卫视", "guizhou"),
                    new ChannelItem("河北卫视", "hebei"),
                    new ChannelItem("黑龙江卫视", "heilongjiang"),
                    new ChannelItem("河南卫视", "henan"),
                    new ChannelItem("湖北卫视", "hubei"),
                    new ChannelItem("湖南卫视", "hunan"),
                    new ChannelItem("江苏卫视", "jiangsu"),
                    new ChannelItem("江西卫视", "jiangxi"),
                    new ChannelItem("吉林卫视", "jilin"),
                    new ChannelItem("辽宁卫视", "liaoning"),
                    new ChannelItem("旅游卫视", "travel"),
                    new ChannelItem("内蒙古卫视", "neimenggu"),
                    new ChannelItem("宁夏卫视", "ningxia"),
                    new ChannelItem("青海卫视", "qinghai"),
                    new ChannelItem("厦门卫视", "xiamen"),
                    new ChannelItem("山东卫视", "shandong"),
                    new ChannelItem("山西卫视", "shan1xi"),
                    new ChannelItem("陕西卫视", "shan3xi"),
                    new ChannelItem("深圳卫视", "shenzhen"),
                    new ChannelItem("四川卫视", "sichuan"),
                    new ChannelItem("天津卫视", "tianjin"),
                    new ChannelItem("西藏卫视", "xizang"),
                    new ChannelItem("新疆卫视", "xinjiang"),
                    new ChannelItem("延边卫视", "yanbian"),
                    new ChannelItem("云南卫视", "yunnan"),
                    new ChannelItem("浙江卫视", "zhejiang")
                }
            },
            new ChannelGroup()
            {
                Name = "广东频道",
                Channels = new ObservableCollection<ChannelItem>()
                {
                    new ChannelItem("广东卫视", "1"),
                    new ChannelItem("广东珠江", "2"),
                    new ChannelItem("广东新闻", "6"),
                    new ChannelItem("广东公共", "4"),
                    new ChannelItem("广东体育", "3"),
                    new ChannelItem("南方卫视(地面版)", "14"),
                    new ChannelItem("南方卫视(上星版)", "15"),
                    new ChannelItem("经济科教", "13"),
                    new ChannelItem("广东影视", "17"),
                    new ChannelItem("广东综艺", "16"),
                    new ChannelItem("广东国际", "8"),
                    new ChannelItem("珠江境外", "5"),
                    new ChannelItem("广东少儿", "18"),
                    new ChannelItem("嘉佳卡通", "7"),
                    new ChannelItem("现代教育", "31"),
                    new ChannelItem("广东移动", "32")
                }
            }
        };
        #endregion
    }
}
