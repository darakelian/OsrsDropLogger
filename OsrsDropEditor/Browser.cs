using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsrsDropEditor
{
    class Browser
    {

        internal HtmlDocument _document;
        public HtmlNode Document { get { return _document.DocumentNode; } }

        public string InnerText { get { return _document.DocumentNode.InnerText; } }

        internal string _uri;
        public string Uri { get { return _uri; } }

        public Browser()
        {
            _document = new HtmlDocument();
        }

        public void Navigate(string url, bool absolute = false)
        {
            HttpWebRequest request = WebRequest.Create( absolute ? url : OsrsDataContainers.OsrsWikiBase + url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string responseString = reader.ReadToEnd();
                _document.LoadHtml(responseString);
                _uri = request.RequestUri.ToString();
            }
        }

        public HtmlNodeCollection SelectNodes(string xpath)
        {
            return _document.DocumentNode.SelectNodes(xpath);
        }

        public HtmlNode SelectSingleNode(string xpath)
        {
            return _document.DocumentNode.SelectSingleNode(xpath);
        }

    }
}
