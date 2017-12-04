using Sgml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace OsrsDropEditor
{
    class Browser
    {

        internal XmlDocument _document;
        public XmlDocument Document { get { return _document; } }

        internal SgmlReader _sgmlReader;

        public string InnerText { get { return _document.InnerText; } }

        internal string _uri;
        public string Uri { get { return _uri; } }

        internal bool _expectNonHtmlResponse;
        public bool ExpectNonHtmlResponse { get { return _expectNonHtmlResponse; } set { _expectNonHtmlResponse = value; } }

        public Browser()
        {
            _sgmlReader = new SgmlReader();
            _sgmlReader.DocType = "HTML";
            _sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            _sgmlReader.CaseFolding = CaseFolding.ToLower;
        }

        public void Navigate(string url, bool absolute = false)
        {
            HttpWebRequest request = WebRequest.Create(absolute ? url : OsrsDataContainers.OsrsWikiBase + url) as HttpWebRequest;
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                    _sgmlReader.InputStream = reader;
                    _document = new XmlDocument();
                    _document.PreserveWhitespace = true;
                    _document.XmlResolver = null;
                    if (_expectNonHtmlResponse)
                    {
                        XmlNode wrappedJson = _document.CreateNode("element", "content", "");
                        wrappedJson.InnerText = _sgmlReader.ReadOuterXml();
                        _document.AppendChild(wrappedJson);
                    }
                    else
                        _document.Load(_sgmlReader);

                    _uri = request.RequestUri.ToString();
                }
            }
            catch (WebException)
            {
                Console.WriteLine("Unable to navigate to webpage.");
            }
        }

        public IEnumerable<XmlNode> SelectNodes(string xpath)
        {
            return _document.SelectNodes(xpath).Cast<XmlNode>();
        }

        public IEnumerable<XmlNode> SelectNodes(XmlNode node, string xpath)
        {
            return node.SelectNodes(xpath).Cast<XmlNode>();
        }

        public XmlNode SelectSingleNode(string xpath)
        {
            return _document.SelectSingleNode(xpath);
        }

    }
}
