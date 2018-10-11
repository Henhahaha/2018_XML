using OpenDataImport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace homework
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = findOpenData();
            ShowOpenData(nodes);
            // Console.ReadKey();
            Console.ReadLine();
        }

        static List<OpenData> findOpenData()
        {
            List<OpenData> result = new List<OpenData>();

            var xml = XElement.Load(@"C:\Users\Henry\Downloads\O-A0001-001.xml");

            XNamespace aw = "urn:cwb:gov:tw:cwbcommon:0.1";
            var nodes = xml.Descendants(aw + "location").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                OpenData item = new OpenData();

                //item.id = int.Parse(getValue(node, "id"));
                item.地區 = getValue(node,aw + "locationName");
                item.經度 = getValue(node,aw + "lat");
                item.緯度= getValue(node,aw + "lon");
                result.Add(item);
            }
            return result;
        }

        private static string getValue(XElement node, XName propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }

        public static void ShowOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            /*nodes.GroupBy(node => node.地區).ToList()
                 .ForEach(group =>
                 {
                     var key = group.Key;
                     var groupDatas = group.ToList();
                     var message = $"服務分類:{key},共有{groupDatas.Count()}筆資料";
                     Console.WriteLine(message);
                 });*/
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                Console.WriteLine(string.Format("{0}.{1}", i + 1, node.地區));
                Console.WriteLine(string.Format("\t經度:{0}", node.經度));
                Console.WriteLine(string.Format("\t緯度:{0}", node.緯度));
            }
        }
    }
}
