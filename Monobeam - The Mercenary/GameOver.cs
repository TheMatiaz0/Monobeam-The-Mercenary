using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TheMatiaz0_MonobeamTheMercenary;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.Audio;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class GameOver
    {
        public static event EventHandler<bool> OnGameOverChanged = delegate { };

        public static bool IsGameOver
        {
            get => _IsGameOver;
            set
            {
                _IsGameOver = value;
                OnGameOverChanged(null, value);
            }

        }

        private static bool _IsGameOver;

        


        public static void Death()
        {
            if (IsGameOver)
            {
                Console.Clear();

                Console.SetCursorPosition(10, 10);
                Console.ForegroundColor = ConsoleColor.Red;

                AudioSystem.Play("368367__thezero__game-over-sound.wav", AudioChannel.Music);

                Console.WriteLine(@"
▓█████▄ ▓█████ ▄▄▄     ▄▄▄█████▓ ██░ ██ 
▒██▀ ██▌▓█   ▀▒████▄   ▓  ██▒ ▓▒▓██░ ██▒
░██   █▌▒███  ▒██  ▀█▄ ▒ ▓██░ ▒░▒██▀▀██░
░▓█▄   ▌▒▓█  ▄░██▄▄▄▄██░ ▓██▓ ░ ░▓█ ░██ 
░▒████▓ ░▒████▒▓█   ▓██▒ ▒██▒ ░ ░▓█▒░██▓
 ▒▒▓  ▒ ░░ ▒░ ░▒▒   ▓▒█░ ▒ ░░    ▒ ░░▒░▒
 ░ ▒  ▒  ░ ░  ░ ▒   ▒▒ ░   ░     ▒ ░▒░ ░
 ░ ░  ░    ░    ░   ▒    ░       ░  ░░ ░
   ░       ░  ░     ░  ░         ░  ░  ░
 ░                                      
");

                ConsoleKeyInfo KeyInfo;
                KeyInfo = Console.ReadKey(true);

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        IsGameOver = false;
                        Console.Clear();
                        break;
                }

                Thread.Sleep(999);
            }
        }

        
    }
}
