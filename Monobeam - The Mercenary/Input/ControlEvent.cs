using System;
using System.Collections;
using System.Collections.Generic;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;

namespace TheMatiaz0_MonobeamTheMercenary.Input
{
    public static class ControlEvent
    {
        public delegate void Action();

        static public Action EventToLoad { get; set; }

        public static void SelectOptions(params Option[] options) => SelectOptions(null, false, "", options);

        public static void SelectOptions(bool mainMenu, string content = "", params Option[] options) => SelectOptions(null, true, content, options);

        private readonly static List<string> ChoicesText = new List<string>();

        private static void SelectOptions(Action ifNull, bool mainMenu = false, string content = "", params Option[] options)
        {
            if (ifNull == null)
            {
                if (options == null || options.Length == 0 || options[0].Text == default)
                {
                    throw new Exception("No options");
                }
            }
            else
            {
                EventToLoad = new Action(ifNull);
                return;
            }

            int x = 1;

            ChoicesText.Clear();

            for (int i = 0; i < options.Length; i++)
            {
                if (mainMenu == false)
                {
                    Console.ForegroundColor = options[i].Color;
                    Console.WriteLine($"{x++}. {options[i].Text}");
                }

                else
                {
                    ChoicesText.Add($"{x++}. {options[i].Text}");
                }

            }

            if (mainMenu == true)
            {
                string[] additionalContent = new string[] { content };

                if (string.IsNullOrEmpty(content))
                {
                    GUI.DrawMenu(ChoicesText.ToArray());
                }

                else
                {
                    GUI.DrawMenu(additionalContent, ChoicesText.ToArray());
                }

            }

            options[GUI.GetNumber(options.Length, 1) - 1].EventToLoad();
        }

    }
}