using System;
using TheMatiaz0_MonobeamTheMercenary.Serialization;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.Translation;
using TheMatiaz0_MonobeamTheMercenary.Input;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class OptionsButtons
    {
        public static void AudioBeep()
        {
            Console.Clear();

            ControlEvent.SelectOptions
     (true, "", new Option(TranslationSystem.GUIFromFile["SwitchON"], () => AudioBeepSwitch(true)),
     new Option(TranslationSystem.GUIFromFile["SwitchOFF"], () => AudioBeepSwitch(false)),
     new Option(TranslationSystem.GUIFromFile["Back"], Program.Options));
        }

        public static void Language()
        {
            Console.Clear();

            Console.WriteLine($"\n \n{TranslationSystem.GUIFromFile["LanguageSetOption"]}");
            string language = Console.ReadLine();
            OptionsManager.Language = language;

            Back(Program.Options);
        }

        public static void TypewriterEffect()
        {
            Console.Clear();

            Console.WriteLine($"\n \n{TranslationSystem.GUIFromFile["DelaySetOption"]}");

            OptionsManager.TypewriterEffectSpeed = (uint)GUI.GetNumber(200, 0);

            Back(Program.Options);

        }

        #region Audio
        public static void Music()
        {
            Console.Clear();

            Console.WriteLine($"\n \n{TranslationSystem.GUIFromFile["MusicSetOption"]}");

            OptionsManager.Music = (uint)GUI.GetNumber(100, 0);

            Back(Program.Options);
        }

        public static void Sounds()
        {
            Console.Clear();

            Console.WriteLine($"\n \n{TranslationSystem.GUIFromFile["SoundSetOption"]}");

            OptionsManager.Sounds = (uint)GUI.GetNumber(100, 0);

            Back(Program.Options);
        }

        #endregion

        static void AudioBeepSwitch(bool isTrue)
        {
            OptionsManager.BeepSound = isTrue;
            Back(Program.Options);
        }

        static void Back(ControlEvent.Action @event)
        {
            SaveSystem.SaveSettings();

            ControlEvent.EventToLoad = @event;
            ControlEvent.EventToLoad();
        }


    }
}
