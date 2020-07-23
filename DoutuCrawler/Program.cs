using DoutuCrawler.Helper;
using DoutuCrawler.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoutuCrawler
{
    class Program
    {
        static DoutuEntities doutuEntities = new DoutuEntities();
        static void Main(string[] args)
        {
            for(var q = 0; q < 100; q++)
            {
                string html = HttpHelper.DownloadUrl(@"https://www.52doutu.cn/rand/");
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                List<HtmlNode> htmlNodes = document.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[1]/div").ToList();
                var count = htmlNodes.Count;

                for (var i = 0; i < count; i++)
                {
                    var path = "//*[@id=\"container\"]/div[1]/div[" + (i + 1) + "]/a/div/img";
                    var node = htmlNodes[i].SelectSingleNode(path);
                    var alt = node.Attributes["alt"].Value;
                    var dataOriginal = node.Attributes["data-original"].Value;
                    var dataBackup = htmlNodes[i].SelectSingleNode(path).Attributes["data-backup"].Value;

                    doutuEntities.Set<Info>().Add(new Info()
                    {
                        Name = alt,
                        Url = dataOriginal,
                        UrlBackup = dataBackup
                    });
                    Console.WriteLine("(" + q + "-" + i + ") " + DateTime.Now + "：" + alt);
                }
            }
            doutuEntities.SaveChanges();
        }
    }
}
