using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Infrastructure
{
    public static class LogService
    {
        public static void SaveToLog(string text)
        {
            try
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                using (var sw = new StreamWriter(folder + @"\Log.txt", true))
                {
                    sw.WriteLine(text + " : " + DateTime.Now);
                }
            }
            catch { }
        }
    }
}
