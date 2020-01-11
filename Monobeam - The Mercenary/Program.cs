using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using TheMatiaz0_MonobeamTheMercenary.Audio;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;
using TheMatiaz0_MonobeamTheMercenary.NPC;
using TheMatiaz0_MonobeamTheMercenary.Progression;
using TheMatiaz0_MonobeamTheMercenary.Serialization;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;


namespace TheMatiaz0_MonobeamTheMercenary
{
    /// <summary>
    /// Main core of the Console game.
    /// </summary>
    [Serializable]
    public class Program
    {
        /// <summary>
        /// All enemies killed in simple list.
        /// </summary>
        public static ObservableCollection<Enemy> EnemiesKilled { get; set; } = new ObservableCollection<Enemy>();

        /// <summary>
        /// Variables used to remove Minimize and Maximize option in this console game. 
        /// </summary>
        #region Minimize/Maximize disable

        private const int MF_BYCOMMAND = 0x00000000;

        public const int SC_MINIMIZE = 0xF020, SC_MAXIMIZE = 0xF030;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion

        /// <summary>
        /// Character instance (use it anywhere, where you need player variables, etc.).
        /// </summary>
        public static Character Character { get; set; }

        public static Narrator Narrator { get; set; } = new Narrator();

        /// <summary>
        /// Variable that comes from 'input' answer.
        /// </summary>
        public static string TempVariable { get; set; }

        public static Thread MainThread;

        /// <summary>
        /// Random instance (use it anywhere you need to randomize stuff).
        /// </summary>
        public static Random Random { get; } = new Random();

        /// <summary>
        /// RegionEntities is for any IPositionEntities (except player) on player position.
        /// </summary>
        public static List<IPositionEntity> NearbyEntities = new List<IPositionEntity>();

        /// <summary>
        /// Can player type any command right now?
        /// </summary>
        public static bool TypeCommand = true;

        /// <summary>
        /// Slot number for your actual game save.
        /// </summary>
        private static int SlotNumber = 0;

        public static bool DebugMode = true;


        /// <summary>
        /// If player selected to load the game... load slot number.
        /// </summary>
        private static void LoadGame()
        {
            GUI.DrawMenu("Type slot number for your save file...");
            SlotNumber = GUI.GetNumber(99, 0);
            SaveSystem.Load(SlotNumber);
        }

        /// <summary>
        /// Options, used in Pause menu and in Main Menu.
        /// </summary>
        public static void Options()
        {
            Console.Clear();

            List<Option> optionsMainMenu = new List<Option>
            {
                {
                    new Option($"{GUIFromFile["Language"]} {OptionsManager.Language}", OptionsButtons.Language),
                    new Option($"{GUIFromFile["AudioBeep"]} {OptionsManager.BeepSound}", OptionsButtons.AudioBeep),
                    new Option($"{GUIFromFile["MusicVolume"]} {OptionsManager.Music}", OptionsButtons.Music),
                    new Option($"{GUIFromFile["SoundVolume"]} {OptionsManager.Sounds}", OptionsButtons.Sounds),
                    new Option($"{GUIFromFile["TypingEffect"]} {OptionsManager.TypewriterEffectSpeed}", OptionsButtons.TypewriterEffect)
                }
            };

            if (GUI.OpenedPauseMenu == false)
            {
                optionsMainMenu.Add(new Option(GUIFromFile["Back"], MainMenu));
            }

            else
            {
                optionsMainMenu.Add(new Option(GUIFromFile["Back"], GUI.PauseMenu));
            }

            ControlEvent.SelectOptions(true, "", optionsMainMenu.ToArray());

        }


        /// <summary>
        /// Credits menu.
        /// </summary>
        private static void Credits()
        {
            Console.Clear();

            ControlEvent.SelectOptions
    (true, GUIFromFile["CreditsInternal"], new Option(GUIFromFile["Back"], MainMenu));
        }

        /// <summary>
        /// Quit application.
        /// </summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }

        private static void MainMenu()
        {
            Console.Clear();

            ControlEvent.SelectOptions
                (true, "", new Option($"{GUIFromFile["Start_Game"]}", StartGame),
                new Option($"{GUIFromFile["Load_Game"]}", LoadGame),
                new Option($"{GUIFromFile["Options"]}", Options),
                new Option($"{GUIFromFile["Credits"]}", Credits),
                new Option($"{GUIFromFile["Exit"]}", Exit));
        }

        /// <summary>
        /// If player choosed to start the game... create a save file with slot number.
        /// </summary>
        private static void StartGame()
        {
            Console.Clear();

            GUI.DrawMenu($"{GUIFromFile["SlotNumber"]}");

            SlotNumber = GUI.GetNumber(99, 0);

            SaveSystem.CreateSlot(SlotNumber);

            Console.Clear();

            ControlEvent.EventToLoad = () => World(Region.Winterside);
            ControlEvent.EventToLoad();
        }

        private static void World(Region actualRegion)
        {
            SpawnRegionEntities.Clear();
            AudioSystem.Stop();

            Inventory.AddItem<OldRustySword>(2);
            // Inventory.AddItem<DustyDirtyHelmet>(1);

            /*
            QuestManager.AddQuest<Good__because_Polish>();
            QuestManager.AddQuest<Zombieez>();
            */

            /*
            Inventory.AddItem<Sausage>(10);
            */

            Inventory.AddItem<OldRustySword>(5);

            switch (actualRegion)
            {
                case Region.Winterside:

                    if (Character == null)
                    {
                        if (DebugMode)
                        {
                            Character = new Warrior("Test", Gender.Male);
                        }

                        else
                        {
                            Character = new Warrior(TempVariable, Gender.Male);
                        }

                    }

                    // Inventory.AddItem<Sausage>(3);

                    // SpawnRegionEntities.AddEntity<StrangerFirstNPC>(new Point(0, 0));

                    CommandSystem.CollisionHandler();

                    SpawnRegionEntities.AddEntity<BlacksmithRoger>(new Point(0, 1));
                    SpawnRegionEntities.AddEntity<ZombieThomas>(new Point(5, 0));
                    SpawnRegionEntities.AddEntity<PoniaczSeba>(new Point(-5, 0));

                    GUI.CommandActive();

                    break;
            }
        }

        public static void ChangedEnemiesKilled(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (KillQuest killQuest in
                from item in QuestManager.ActiveQuests
                where item is KillQuest
                select item as KillQuest
                )
                {
                    QuestManager.KillQuestComplete(killQuest);
                    break;
                }
            }
        }

        public static void ChangedItemsPicked()
        {
            foreach (GatherQuest gatherQuest in
            from item in QuestManager.ActiveQuests
            where item is GatherQuest
            select item as GatherQuest
            )
            {
                QuestManager.GatherQuestComplete(gatherQuest);
                break;
            }
        }


        /// <summary>
        /// Start method.
        /// </summary>
        private static void Main()
        {
            #region Initialization

            // Remove Maximize and Minimize buttons.
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);

            // Set resolution.
            Console.WindowHeight = 30;
            Console.WindowWidth = 120;

            Console.Title = "Monobeam: The Mercenary";

            // Create settings file.
            SaveSystem.CreateSettingsFile();

            // Gamepad input
            Gamepad inputMonitor = new Gamepad();
            inputMonitor.Start();

            GUI.OnOpenedInventory += (s, e) => GUI.InventoryMenu();
            GameOver.OnGameOverChanged += (v, e) => GameOver.Death();
            GUI.OnOpenedPauseMenu += (s, e) => GUI.PauseMenu();

            Inventory.OnItemPicked += (s, e) => ChangedItemsPicked();


            SaveSystem.LoadSettings();

            // Get all translation files:
            GetJsonFiles();


            EnemiesKilled.CollectionChanged += ChangedEnemiesKilled;

            // Play Main Theme.
            AudioSystem.Play("458368__legend1060__sci-fi-theme.wav", AudioChannel.Music, true);

            // Load Main Menu:
            ControlEvent.EventToLoad = MainMenu;

            ControlEvent.EventToLoad();


            #endregion
        }
    }

}
