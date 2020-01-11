using System;
using System.Collections.Generic;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using System.Diagnostics;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public class QuestManager
    {
        public static readonly List<Quest> ActiveQuests = new List<Quest>();
        public static readonly List<Quest> CompletedQuests = new List<Quest>();
        public static readonly List<Quest> FailedQuests = new List<Quest>();

        internal static Quest GetQuestByID(Guid id)
        {
            return ActiveQuests.FirstOrDefault(quest => quest.ID == id);
        }

        public static uint QuestsPicked { get; private set; }

        private const int maxQuests = 3;

        public static bool Exists<T>() where T : Quest => ActiveQuests.Any(item => item is T);

        public static bool Exists(Type type) => (ActiveQuests.Exists(item => item.GetType() == type));

        public static void AddQuest(Quest quest)
        {
            if (ActiveQuests.Count < maxQuests)
            {
                Debug.WriteLine($"Added quest {quest.Name}!");
                ActiveQuests.Add(quest);
            }

            else
            {
                throw new Exception("There is no more space in the Quest Manager.");
            }
        }

        public static void AddQuest<T>() where T : Quest, new() => AddQuest(new T());

        public static void RemoveQuest<T>(bool failed) where T : Quest, new() => RemoveQuest(new T(), failed);

        public static void RemoveQuest(Quest quest, bool failed = false)
        {
            ActiveQuests.Remove(quest);

            if (!failed)
            {
                CompletedQuests.Add(quest);
            }

            else
            {
                FailedQuests.Add(quest);
            }
        }

        public static void GetReward<T>() where T : Quest, new() => GetReward(new T());

        public static void GetReward(Quest quest)
        {
            if (ActiveQuests.Exists(questRecord => (questRecord.Name == quest.Name)))
            {
                Console.WriteLine("\n");

                GUI.GenerateGUI('-', 120);

                GUI.ColoredWriteLine(ConsoleColor.DarkYellow, $"\n Woah! You have completed quest - \"{quest.Name}\"! Congrats!");
                GUI.ColoredWriteLine(ConsoleColor.DarkYellow, $"Rewards:\n - {quest.EXPReward} EXP points \nItems:");

                foreach (InventoryRecord item in quest.RewardItems)
                {
                    GUI.ColoredWriteLine(ConsoleColor.DarkYellow, $"- {item.InventoryItem.Name} x{item.Quantity}\n \n");
                    Inventory.AddItem(item, item.Quantity);
                }

                Program.Character.AddEXP(quest.EXPReward);

                GUI.GenerateGUI('-', 120);

                RemoveQuest(quest);
            }
        }

        public static void GatherQuestComplete(GatherQuest gatherQuest)
        {
            if (ActiveQuests.Exists(questRecord => (questRecord.Name == gatherQuest.Name)))
            {
                Debug.WriteLine("Completed!");

                IEnumerable<InventoryRecord> itemsList = gatherQuest.ItemsToGather.Except(Inventory.Items);


                foreach (InventoryRecord item in gatherQuest.ItemsToGather)
                {
                    if (itemsList.Contains(item))
                    {
                        GetReward(gatherQuest);
                        break;
                    }
                }
            }
        }

        public static void KillQuestComplete (KillQuest killQuest)
        {
            if (ActiveQuests.Exists(questRecord => (questRecord.Name == killQuest.Name)))
            {
                // Różnice w obu listach:
                IEnumerable<Enemy> differenceList = killQuest.Targets.Except(Program.EnemiesKilled);

                foreach (Enemy item in killQuest.Targets)
                {
                    if (differenceList.Contains(item))
                    {
                        GetReward(killQuest);
                        break;
                    }
                }

            }
        }
    }
}
