using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Serialization;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    /// <summary>
    /// Get translated text from this class.
    /// </summary>
    public class TranslationSystem
    {
        /// <summary>
        /// Dialogues with line number.
        /// </summary>
        public static DialogueStructure DialogueFromFile = new DialogueStructure();

        /// <summary>
        /// GUI translations with definitions.
        /// </summary>
        public static Dictionary<string, string> GUIFromFile = new Dictionary<string, string>();

        public static CommandStructure CommandFromFile;

        public static InventoryStructure ItemFromFile = new InventoryStructure();

        public static string[] Languages = new[]
        {
            "Polish",
            "English"
        };

        // Jeden string[] odpowiada za pliki, ich zbiór. Drugi za język.
        public static Dictionary<string, string[]> LanguageFile = new Dictionary<string, string[]>();

        /// <summary>
        /// Get all JSON files with translation.
        /// </summary>
        public static void GetJsonFiles()
        {
            string[] files = Directory.GetFiles($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Monobeam The Mercenary/Translation", "*", SearchOption.AllDirectories).Where(item => Path.GetExtension(item) == ".json").ToArray();

            foreach (string language in Languages)
            {
                LanguageFile[language] = files.Where(item => item.Contains(language)).ToArray();
            }

            DialogueFromFile = SerializationJSON.ReadFromJsonFile<DialogueStructure>(LanguageFile[OptionsManager.Language].ToList().Find(item => item.Contains("Dialogues")));
            GUIFromFile = SerializationJSON.ReadFromJsonFile<Dictionary<string, string>>(LanguageFile[OptionsManager.Language].ToList().Find(item => item.Contains("GUI")));
            CommandFromFile = SerializationJSON.ReadFromJsonFile<CommandStructure>(LanguageFile[OptionsManager.Language].ToList().Find(item => item.Contains("Commands")));
            ItemFromFile = SerializationJSON.ReadFromJsonFile<InventoryStructure>(LanguageFile[OptionsManager.Language].ToList().Find(item => item.Contains("Items")));
        }
    }
}
