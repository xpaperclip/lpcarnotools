using System;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace LxTools.Liquipedia
{
    public static class LiquipediaClientEx
    {
        public static string LoginGetEditToken(string username, string password)
        {
            string xml = MakeRequest("format=xml&action=login&lgname={0}&lgpassword={1}", username, password);
            return XDocument.Parse(xml).Element("api").Element("login").Attribute("token").Value;
        }
        public static void EditPage(string title, string text, string summary, string token)
        {
            MakeRequest("format=xml&action=edit&title={0}&text={1}&summary={2}&token={3}", Uri.EscapeDataString(title), Uri.EscapeDataString(text), Uri.EscapeDataString(summary), Uri.EscapeDataString(token));
        }

        public static string RequestParse(string page)
        {
            string xml = MakeRequest("format=xml&action=parse&text={0}", Uri.EscapeDataString(page));
            return XDocument.Parse(xml).Element("api").Element("parse").Element("text").Value;
        }
        public static string GetPageContent(string page)
        {
            string xml = MakeRequest("format=xml&action=query&prop=revisions&rvprop=content&titles={0}", Uri.EscapeDataString(page));
            return XDocument.Parse(xml).Element("api").Element("query").Element("pages").Element("page").Element("revisions").Element("rev").Value;
        }

        private static string MakeRequest(string query, params string[] args)
        {
            return MakeRequest(string.Format(query, args));
        }
        private static string MakeRequest(string query)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create("http://wiki.teamliquid.net/starcraft2/api.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.WriteLine(query);
            }

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
