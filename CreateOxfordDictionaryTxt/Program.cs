using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;

using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using AngleSharp.Browser.Dom;
using Microsoft.EntityFrameworkCore;
using CreateOxfordDictionaryTxt.DataBaseCore;
using CreateOxfordDictionaryTxt.GetDataFromSource;

namespace CreateOxfordDictionaryTxt
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = $"https://www.oxfordlearnersdictionaries.com/wordlists/oxford3000-5000";
            FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Dictionary.txt");

            string htmlText = await DataFromSource.GetHtmlAsText(url, file);

        //   DataFromSource.WriteData(htmlText, file);

            DataFromSource.WriteData(htmlText, new WordsContext(), true);   
        }
    }
}