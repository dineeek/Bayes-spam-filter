using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace FSpam
{
    public class SpamHamReader
    {
        public List<string> Read(string category)
        {
            List<string> list = new List<string>();

            List<string> resourceNames = this.GetType().Assembly.GetManifestResourceNames().ToList();
            resourceNames.RemoveRange(0, 3);//prva tri resursa miče koji nisu emailovi

            List<string> hamResourceNames = new List<string>();
            List<string> spamResourceNames = new List<string>();

            foreach(var resName in resourceNames)
            {
                if (resName.Contains("ham"))
                    hamResourceNames.Add(resName);
                else
                    spamResourceNames.Add(resName);
            }

            if (category == "ham")
            {
                foreach (string resourceName in hamResourceNames)
                {
                    Assembly myAssembly = Assembly.GetExecutingAssembly();
                    using (var streamReader = new StreamReader(myAssembly.GetManifestResourceStream(resourceName), Encoding.Default, true))
                    {
                        var text = streamReader.ReadToEnd();
                        list.Add(text);
                    }
                }
            }
            else
            {
                foreach (string resourceName in spamResourceNames)
                {
                    Assembly myAssembly = Assembly.GetExecutingAssembly();
                    using (var streamReader = new StreamReader(myAssembly.GetManifestResourceStream(resourceName), Encoding.Default, true))
                    {
                        var text = streamReader.ReadToEnd();
                        list.Add(text);
                    }
                }
            }

            return list;           
        }
    }
}
