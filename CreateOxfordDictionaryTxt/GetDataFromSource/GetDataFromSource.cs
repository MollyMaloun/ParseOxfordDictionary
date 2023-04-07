using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using AngleSharp.Io;
using CreateOxfordDictionaryTxt.DataBaseCore;
using Microsoft.EntityFrameworkCore;

namespace CreateOxfordDictionaryTxt.GetDataFromSource
{
    static class DataFromSource
    {
        public static async Task<string> GetHtmlAsText(string url, FileInfo file, bool showDataToConsole = false)
        {
            string HtmlText = string.Empty;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HtmlText = await response.Content.ReadAsStringAsync();
            }
            

            if (showDataToConsole)
            {
                Console.WriteLine(HtmlText);
            }
            return HtmlText;
        }

        public static async Task WriteData(string htmlText, FileInfo file, bool showDataToConsole = false)
        {
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(htmlText);
            var dataTable = doc.QuerySelectorAll("ul").Where(item => item.ClassName == "top-g");
            List<string> data = new List<string>();

            foreach (var items in dataTable)
            {

                foreach (var item in items.ChildNodes.QuerySelectorAll("a"))
                {
                    data.Add(item.TextContent);
                }
            }
            
            data = data.Distinct().ToList();

            
            foreach (var item in data)
            {
                using (StreamWriter sw = file.AppendText())
                {
                    sw.WriteLine(item);
                    if(showDataToConsole)
                    {
                        await Console.Out.WriteLineAsync(item);
                    }
                }
            }
        }

        public static async Task WriteData(string htmlText, WordsContext wordsContext ,bool showDataToConsole = false)
        {
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(htmlText);
            var table = doc.QuerySelectorAll("ul").Where(item => item.ClassName == "top-g");
            List<string> data = new List<string>();

            foreach (var items in table)
            {

                foreach (var item in items.ChildNodes.QuerySelectorAll("a"))
                {
                    data.Add(item.TextContent);

                }
            }  
            
            data = data.Distinct().ToList();

            int idKey = 0;
            foreach (var item in data)
            {
                Words word = new Words { id = ++idKey, word = item };
                wordsContext.engwords.Add(word);
                wordsContext.SaveChanges();
                if (showDataToConsole)
                {
                    Console.WriteLine(word.word);
                }
            }
        }
    }
}
