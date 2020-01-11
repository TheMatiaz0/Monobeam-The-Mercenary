using System;
using System.Collections.Generic;
using System.Threading;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class GUI
    {
        private static readonly List<Option> Options = new List<Option>();

        public static bool OpenedInventory
        {
            get => _OpenedInventory;
            set
            {
                _OpenedInventory = value;
                OnOpenedInventory(null, value);
            }

        }

        private static bool _OpenedInventory;


        public static event EventHandler<bool> OnOpenedInventory = delegate { };

        public static bool OpenedPauseMenu
        {
            get => _OpenedPauseMenu;

            set
            {
                _OpenedPauseMenu = value;
                OnOpenedPauseMenu(null, value);
            }
        }

        private static bool _OpenedPauseMenu;

        public static event EventHandler<bool> OnOpenedPauseMenu = delegate { };


        public static void GenerateGUI(string text, int multiple)
        {
            for (int x = 0; x < multiple; x++)
            {
                Console.Write(text);
            }
        }

        public static void GenerateGUI(char symbol, int multiple)
        {
            GenerateGUI($"{symbol}", multiple);
        }

        /// <summary>
        /// Usa this for Typewriter Effect.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="delay"></param>
        public static void PrintTextWithEffect(string text, int delay)
        {
            foreach (char v in text)
            {
                Console.Write(v);

                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(delay);
                }
            }

            if (Console.KeyAvailable)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Draw slots of inventory with action: Use.
        /// </summary>
        /// <param name="options"></param>
        public static void DrawSlotsUsage(List<Option> options)
        {
            options.Clear();

            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                options.Add(new Option($"{Inventory.Items[i].InventoryItem.Name} (x{Inventory.Items[i].Quantity}) | Sell Price: {(Inventory.Items[i].InventoryItem.PriceToBuy / 2) * Inventory.Items[i].Quantity}\n", Inventory.Items[i].InventoryItem.Use));
            }
        }

        public static void DrawSlotsEat(List<Option> options)
        {
            options.Clear();

            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                if (Inventory.Items[i].InventoryItem is FoodItem)
                {
                    options.Add(new Option($"{Inventory.Items[i].InventoryItem.Name} (x{Inventory.Items[i].Quantity}) | {(Inventory.Items[i].InventoryItem as FoodItem).AddHP} HP\n", Inventory.Items[i].InventoryItem.Eat));
                }
            }
        }

        public static void InventoryMenu()
        {
            if (OpenedInventory == true)
            {
                Program.TypeCommand = false;
                Console.Clear();
                GenerateGUI('_', 120);
                Console.SetCursorPosition(12, 4);


                DrawSlotsUsage(Options);

                Options.Add(new Option(Translation.TranslationSystem.GUIFromFile["Back"], PauseMenuButtons.Resume));

                ControlEvent.SelectOptions(true, @"_  _      _     _____ _      _____ ____  ____ ___  _
/ \/ \  /|/ \ |\/  __// \  /|/__ __Y  _ \/  __\\  \//
| || |\ ||| | //|  \  | |\ ||  / \ | / \||  \/| \  / 
| || | \||| \// |  /_ | | \||  | | | \_/||    / / /  
\_/\_/  \|\__/  \____\\_/  \|  \_/ \____/\_/\_\/_/   
                                                     ", Options.ToArray());
                GenerateGUI('_', 120);
            }

            else if (OpenedInventory == false)
            {
                Program.TypeCommand = true;
                CommandActive();
            }

        }

        public static void PauseMenu()
        {
            Console.Clear();

            if (OpenedPauseMenu == true)
            {
                Program.TypeCommand = false;
                ControlEvent.SelectOptions
    (true, @"
 .___      .    .     .   _____ .____  .___  
 /   \    /|    /     /  (      /      /   ` 
 |,_-'   /  \   |     |   `--.  |__.   |    |
 |      /---'\  |     |      |  |      |    |
 /    ,'      \  `._.'  \___.'  /----/ /---/ 
                                             ", new Option(Translation.TranslationSystem.GUIFromFile["PauseButton1"], PauseMenuButtons.Resume),
    new Option(Translation.TranslationSystem.GUIFromFile["PauseButton2"], Program.Options),
    new Option(Translation.TranslationSystem.GUIFromFile["PauseButton3"], Program.Exit));
            }

            else if (OpenedPauseMenu == false)
            {
                Program.TypeCommand = true;
                CommandActive();
            }
        }

        public static int GetNumberWithInput(int maxNumber, int minNumber, string input)
        {
            bool isTrue = false;
            int tempVariable = 0;

            if (isTrue == false)
            {
                if (!int.TryParse(input, out tempVariable) || tempVariable > maxNumber || tempVariable < minNumber)
                {
                    ColoredWriteLine(ConsoleColor.Red, Translation.TranslationSystem.GUIFromFile["Error1"]);
                }
            }

            return tempVariable;
        }

        public static int GetNumber(int maxNumber, int minNumber)
        {
            bool isTrue = false;
            int tempVariable = 0;

            while (isTrue == false)
            {
                if (!int.TryParse(Console.ReadLine(), out tempVariable) || tempVariable > maxNumber || tempVariable < minNumber)
                {
                    ColoredWriteLine(ConsoleColor.Red, Translation.TranslationSystem.GUIFromFile["Error1"]);
                }

                else
                {
                    isTrue = true;
                }
            }

            return tempVariable;
        }

        public static void ColoredWriteLine(ConsoleColor consoleColorChange, string text, ConsoleColor defaultColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColorChange;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
        }

        public static void ColoredAction(ConsoleColor consoleColorChange, Action action, ConsoleColor defaultColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColorChange;
            action.Invoke();
            Console.ForegroundColor = defaultColor;
        }

        public static void ColoredWrite(ConsoleColor consoleColorChange, string text, ConsoleColor defaultColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColorChange;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        private static void DrawLogo()
        {
            ColoredWriteLine(ConsoleColor.DarkCyan, @"
       __        __   __   ___                ___       ___           ___  __   __   ___            __      
 |\/| /  \ |\ | /  \ |__) |__   /\   |\/| .    |  |__| |__      |\/| |__  |__) /  ` |__  |\ |  /\  |__) \ / 
 |  | \__/ | \| \__/ |__) |___ /~~\  |  | .    |  |  | |___     |  | |___ |  \ \__, |___ | \| /~~\ |  \  |  
                                                                                                            
");
        }


        public static void DrawMenu(string[] textsToWrite, string[] outsideText = null)
        {
            DrawLogo();
            GenerateGUI('-', 120);
            foreach (string text in textsToWrite)
            {
                Console.WriteLine(text);
            }

            GenerateGUI('-', 120);

            if (outsideText != null)
            {
                foreach (string text in outsideText)
                {
                    Console.WriteLine(text);
                }
            }
        }

        /// <summary>
        /// Draw a progress bar at the current cursor position.
        /// </summary>
        /// <param name="progress">The position of the bar</param>
        /// <param name="total">The amount it counts</param>
        public static void DrawTextProgressBar(int progress, int total, string additionalText, ConsoleColor filledPartColor = ConsoleColor.Green, ConsoleColor unfilledPartColor = ConsoleColor.Black)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft += 32;
            Console.Write("]"); //end
            Console.CursorLeft += 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = filledPartColor;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = unfilledPartColor;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"{progress.ToString()} of {total.ToString()}, {additionalText}"); //blanks at the end remove any excess
        }

        public static void DrawMenu(string textToWrite, string outsideText = "")
        {
            DrawLogo();
            GenerateGUI('-', 120);
            Console.WriteLine(textToWrite);
            GenerateGUI('-', 120);
            Console.WriteLine(outsideText);
        }

        /// <summary>
        /// When commands are active.
        /// </summary>
        public static void CommandActive()
        {
            Console.Clear();
            ColoredWriteLine(ConsoleColor.DarkGray, Translation.TranslationSystem.GUIFromFile["TypeCommand"]);

            while (Program.TypeCommand == true)
            {
                CommandSystem.CommandInput(Console.ReadLine());
            }
        }
    }
}
