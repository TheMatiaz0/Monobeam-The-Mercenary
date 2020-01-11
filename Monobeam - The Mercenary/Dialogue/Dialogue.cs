using System;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Serialization;
using System.IO;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Dialogue
{
    public class Dialogue
    {
        private static int Frequency;
        public static bool DialogueSkip;
        private static uint TypeSpeed = 50;

        public static void PrintOutputText(string text, IDialogueEntity entity, bool shouldBreakLine = true, bool shouldBeep = true)
        {
            TypeSpeed = OptionsManager.TypewriterEffectSpeed;

            switch (entity)
            {
                case Narrator _:
                    text = $"{text}";
                    TypeSpeed += 15;
                    break;

                default:
                    text = $"{entity.Name}: {text}";
                    break;
            }

            Console.ForegroundColor = entity.MainColor;
            Frequency = entity.Frequency;

            GUI.PrintTextWithEffect(text, (int)TypeSpeed);

            GUI.ColoredWriteLine(ConsoleColor.DarkGray, $"\n{Translation.TranslationSystem.GUIFromFile["Next"]}");

            if (shouldBeep)
            {
                if (OptionsManager.BeepSound == true)
                {
                    int duration = ((int)OptionsManager.Sounds * 2);
                    Console.Beep(Frequency, duration);
                }
            }

            if (Program.TypeCommand == true)
            {
                if (shouldBreakLine == true)
                {
                    CommandSystem.CommandInput(Console.ReadLine());
                }
            }

        }

        public static void Choice()
        {
            GUI.ColoredWriteLine(ConsoleColor.DarkGray, Translation.TranslationSystem.GUIFromFile["Choice"]);
        }

        public static void PrintInputText(string context)
        {
            GUI.ColoredAction(ConsoleColor.DarkGray, WriteContext);

            void WriteContext()
            {
                Console.Write(context);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.WriteLine(Translation.TranslationSystem.GUIFromFile["TypeText"]);
                Console.SetCursorPosition(Console.CursorLeft + 25, Console.CursorTop - 2);
            }

            Program.TempVariable = Console.ReadLine();
            CommandSystem.CommandInput(Console.ReadLine());
            CommandSystem.CommandInput(Console.ReadLine());
        }
    }
}
