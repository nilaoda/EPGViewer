using EPGViewer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace EPGViewer
{
    class Util
    {
        static readonly HttpClient AppHttpClient = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false,
            AutomaticDecompression = DecompressionMethods.GZip
        })
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        public static async Task<ObservableCollection<ShowItem>> GetShowList(ChannelItem channel, DateTime date)
        {
            var showItems = new ObservableCollection<ShowItem>();
            if (channel == null) return showItems;
            var api = $"http://api.cntv.cn/epg/getEpgInfoByChannelNew?c={channel.Code}&serviceId=tvcctv&d={date.ToString("yyyyMMdd")}&t=jsonp&cb=setItem1&callback=setItem1";
            var resp = await GetWebSource(api);
            resp = resp.Replace("setItem1({", "{").Replace("});", "}");
            var json = JObject.Parse(resp);
            var list = json["data"][channel.Code]["list"].Value<JArray>();
            foreach (var item in list)
            {
                var startTimestamp = item["startTime"].Value<long>();
                var endTimestamp = item["endTime"].Value<long>();
                var startTime = GetDataTimeFromTimestamp(startTimestamp);
                var endTime = GetDataTimeFromTimestamp(endTimestamp);
                showItems.Add(new ShowItem()
                {
                    Name = item["title"].ToString(),
                    Date = date.ToString("yyyy/MM/dd"),
                    StartTime = startTime,
                    EndTime = endTime,
                    Time = $"{startTime.ToString("HH:mm:ss")}-{endTime.ToString("HH:mm:ss")}",
                    StartTimestamp = startTimestamp,
                    EndTimestamp = endTimestamp
                });
            }
            return showItems;
        }

        public static async Task<ObservableCollection<ShowItem>> GetGDShowList(ChannelItem channel, DateTime date)
        {
            var showItems = new ObservableCollection<ShowItem>();
            if (channel == null) return showItems;
            var api = $"http://epg.gdtv.cn/f/{channel.Code}/{date.ToString("yyyy-MM-dd")}.xml";
            var resp = await GetWebSource(api);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resp);
            var json = JObject.Parse(JsonConvert.SerializeXmlNode(doc));
            var list = json["epg"]["epglist"]["content"].Value<JArray>();
            foreach (var item in list)
            {
                var startTimestamp = item["@time1"].Value<long>();
                var endTimestamp = item["@time2"].Value<long>();
                var startTime = GetDataTimeFromTimestamp(startTimestamp);
                var endTime = GetDataTimeFromTimestamp(endTimestamp);
                showItems.Add(new ShowItem()
                {
                    Name = item["#cdata-section"].ToString(),
                    Date = date.ToString("yyyy/MM/dd"),
                    StartTime = startTime,
                    EndTime = endTime,
                    Time = $"{startTime.ToString("HH:mm:ss")}-{endTime.ToString("HH:mm:ss")}",
                    StartTimestamp = startTimestamp,
                    EndTimestamp = endTimestamp
                });
            }
            return showItems;
        }

        private static DateTime GetDataTimeFromTimestamp(long timestamp)
        {
            var t = TimeSpan.FromSeconds(timestamp);
            var b = new DateTime(1970, 1, 1, 0, 0, 0);
            var c = b + t;
            return c.ToLocalTime();
        }

        private static async Task<string> GetWebSource(string url)
        {
            var htmlCode = "";
            try
            {
                using (var webRequest = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    webRequest.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0 Safari/605.1.15");
                    webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                    webRequest.Headers.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                    webRequest.Headers.Connection.Clear();
                    var webResponse = await AppHttpClient.SendAsync(webRequest, HttpCompletionOption.ResponseHeadersRead);
                    htmlCode = await webResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                ;
            }
            return htmlCode;
        }

        public static string GetValidFileName(string input, string re = ".")
        {
            string title = input;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                title = title.Replace(invalidChar.ToString(), re);
            }
            return title;
        }

        public static string FormatItems(string channelName, string templateOri, int marginMinutes, params ShowItem[] showItems)
        {
            var list = new List<string>();
            foreach (var show in showItems)
            {
                var startTime = show.StartTime.AddMinutes(-marginMinutes);
                var endTime = show.EndTime.AddMinutes(marginMinutes);
                var template = templateOri;
                if (template.Contains("${ChannelName}"))
                {
                    template = template.Replace("${ChannelName}", channelName);
                }
                if (template.Contains("${Name}"))
                {
                    template = template.Replace("${Name}", show.Name);
                }
                if (template.Contains("${StartTimestamp}"))
                {
                    template = template.Replace("${StartTimestamp}", (show.StartTimestamp - (marginMinutes * 60 * 1000)).ToString());
                }
                if (template.Contains("${EndTimestamp}"))
                {
                    template = template.Replace("${EndTimestamp}", (show.EndTimestamp + (marginMinutes * 60 * 1000)).ToString());
                }
                //处理自定义日期
                var reg = new Regex("\\${(StartTime|EndTime)\\('(.*?)'\\)}");
                if (reg.IsMatch(template))
                {
                    foreach (Match item in reg.Matches(template))
                    {
                        try
                        {
                            var match = item;
                            var dateTime = match.Value.StartsWith("${StartTime") ? startTime : endTime;
                            template = template.Replace(match.Value, dateTime.ToString(match.Groups[2].Value));
                        }
                        catch (Exception)
                        {
                            template = reg.Replace(template, "日期自定义格式化出错");
                        }
                    }
                }
                //处理函数
                var regFx = new Regex("\\${GetFileName\\('(.*?)'\\)}");
                if (regFx.IsMatch(template))
                {
                    foreach (Match item in regFx.Matches(template))
                    {
                        try
                        {
                            var match = item;
                            template = template.Replace(match.Value, Util.GetValidFileName(match.Groups[1].Value));
                        }
                        catch (Exception)
                        {
                            template = regFx.Replace(template, "函数解析出错");
                        }
                    }
                }
                list.Add(template);
            }
            return string.Join(Environment.NewLine, list);
        }
    }
}
