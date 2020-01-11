using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class Ending
    {
        public static void Error (string code)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"FATAL ERROR (CODE: {code}). KILL PROCESS: {Environment.GetCommandLineArgs()[0]}. Check CrashReport.txt file for more information.");
            Console.Beep(Program.Random.Next(2000, 7000), 5000);
            LogFile.Generate(code);

            Thread.Sleep(4000);

            Environment.Exit(0);
        }
    }
}
