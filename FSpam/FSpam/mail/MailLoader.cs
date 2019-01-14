using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FSpam.mail
{
    class MailLoader
    {
        public List<string> titles = new List<string>();
        public List<string> summaries = new List<string>();
        public List<string> senders = new List<string>();
        public int zastavica = 1;
        public void readMail(string username, string password)
        {
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                string response;
                string title;
                string content;
                string name;
               
                XmlDocument doc = new XmlDocument();

                objClient.Credentials = new System.Net.NetworkCredential(username,password);
                response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                doc.LoadXml(response);

                //MessageBox.Show(response);

                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    title = node.SelectSingleNode("title").InnerText;
                    titles.Add(title);

                    content = node.SelectSingleNode("summary").InnerText;
                    summaries.Add(content);

                    name = node.SelectSingleNode("author/name").InnerText;
                    senders.Add(name);
                }

            }
            catch (Exception exe)
            {
                MessageBox.Show("Neispravno korisničko ime ili lozinka");
                zastavica = 0;
            }
        }
    }
}
