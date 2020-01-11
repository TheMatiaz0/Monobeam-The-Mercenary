using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TheMatiaz0_MonobeamTheMercenary.Progression;
using TheMatiaz0_MonobeamTheMercenary.Audio;

namespace TheMatiaz0_MonobeamTheMercenary.Serialization
{
    public class SaveSystem
    {
        // int = slot, string = path.
        public static Dictionary<int, string> SlotsPaths = new Dictionary<int, string>();

        public static List<object> ListObjects = new List<object>();

        private static string FilePathToSettings;

        public static void CreateSettingsFile ()
        {
            DirectoryInfo di = Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Monobeam The Mercenary/Saves\\Settings");

            FilePathToSettings = $"{di.FullName}/Settings.json";

            if (!File.Exists(FilePathToSettings))
            {
                File.Create(FilePathToSettings);
                File.SetAttributes(FilePathToSettings, FileAttributes.Normal);
            }
        }

        public static void CreateSlot(int slotNumber)
        {
            DirectoryInfo di = Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Monobeam The Mercenary/Saves\\Slot_{slotNumber}");

            SlotsPaths.Add(slotNumber, di.FullName);


            if (!File.Exists($"{SlotsPaths[slotNumber]}/Variables.json"))
            {
                File.Create($"{SlotsPaths[slotNumber]}/Variables.json");
                File.SetAttributes($"{SlotsPaths[slotNumber]}/Variables.json", FileAttributes.Normal);
            }
        }

        public static void Save(int slotNumber)
        {
            SaveObject saveObject = new SaveObject
            {
                Character = Program.Character,
                // CurrentDialogueLine = Translation.TranslationSystem.CurrentDialogueLine
            };

            SerializationJSON.WriteToJsonFile($"{SlotsPaths[slotNumber]}/Variables.json", saveObject);
        }

        public static void SaveSettings ()
        {
            OptionsManager optionsManager = new OptionsManager();

            SerializationJSON.WriteToJsonFile(FilePathToSettings, optionsManager);
        }

        public static void LoadSettings ()
        {
            OptionsManager optionsManagerLoaded = SerializationJSON.ReadFromJsonFile<OptionsManager>(FilePathToSettings);

            OptionsManager.Instance = optionsManagerLoaded;
        }

        public static void Load (int slotNumber)
        {
            SaveObject saveObject = SerializationJSON.ReadFromJsonFile<SaveObject>($"{SlotsPaths[slotNumber]}/Variables.json");

            Program.Character = saveObject.Character;
            // Translation.TranslationSystem.CurrentDialogueLine = saveObject.CurrentDialogueLine;
        }

    }
}
