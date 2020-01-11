using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class LogFile
    {
        private static readonly string filePath = $"{Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])}/CrashReport.txt";
        private static readonly FileInfo fileInfo = new FileInfo(filePath);

       public static void Generate(string errorCode)
       {
            try
            {
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                using (StreamWriter sw = fileInfo.CreateText())
                {
                    sw.WriteLine($"{DateTime.Now.ToString()}: FATAL ERROR (Code: {errorCode}) occured!");
                    sw.WriteLine("Details:");
                    sw.WriteLine("NULL");
                }
            }

            catch (Exception)
            {
                
            }
       }
    }
}
