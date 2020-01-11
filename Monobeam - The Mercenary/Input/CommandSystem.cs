using System;
using System.Collections.Generic;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;
using TheMatiaz0_MonobeamTheMercenary.NPC;
using TheMatiaz0_MonobeamTheMercenary.Progression;
using TheMatiaz0_MonobeamTheMercenary.Translation;

namespace TheMatiaz0_MonobeamTheMercenary.Input
{
    public class CommandSystem
    {
        /// <summary>
        /// Read everything that player types in console. Process it and if it's a command give an ideal outcome.
        /// </summary>
        /// <param name="input"> Any text from player </param>
        public static void CommandInput(string input)
        {
            string onlyCommand;
            string onlyArgs = "";

            if (input.Any(item => char.IsWhiteSpace(item)))
            {
                onlyCommand = input.Remove(input.IndexOf(' '), input.Length - input.IndexOf(' '));
                onlyArgs = input.Remove(0, input.IndexOf(' ') + 1);
            }

            else
            {
                onlyCommand = input;
            }


            if (TranslationSystem.CommandFromFile.Inventory.Any(item => item == onlyCommand.ToUpper()))
            {
                InventoryOpen();
            }

            else if (TranslationSystem.CommandFromFile.Pause.Any(item => item == onlyCommand.ToUpper()))
            {
                Pause();
            }

            else if (TranslationSystem.CommandFromFile.Back.Any(item => item == onlyCommand.ToUpper()))
            {
                Back();
            }

            #region Positions
            else if (TranslationSystem.CommandFromFile.GoNorth.Any(item => item == onlyCommand.ToUpper()))
            {
                Program.Character.Position = new Point(Program.Character.Position.X, Program.Character.Position.Y + 1);
                Movement();
            }

            else if (TranslationSystem.CommandFromFile.GoSouth.Any(item => item == onlyCommand.ToUpper()))
            {
                Program.Character.Position = new Point(Program.Character.Position.X, Program.Character.Position.Y - 1);
                Movement();
            }

            else if (TranslationSystem.CommandFromFile.GoWest.Any(item => item == onlyCommand.ToUpper()))
            {
                Program.Character.Position = new Point(Program.Character.Position.X - 1, Program.Character.Position.Y);
                Movement();
            }

            else if (TranslationSystem.CommandFromFile.GoEast.Any(item => item == onlyCommand.ToUpper()))
            {
                Program.Character.Position = new Point(Program.Character.Position.X + 1, Program.Character.Position.Y);
                Movement();
            }
            #endregion

            else if (TranslationSystem.CommandFromFile.Help.Any(item => item == onlyCommand.ToUpper()))
            {
                Help();
            }

            else if (TranslationSystem.CommandFromFile.Status.Any(item => item == onlyCommand.ToUpper()))
            {
                Status(onlyArgs.ToUpper());
            }

            else if (TranslationSystem.CommandFromFile.LevelUP.Any(item => item == onlyCommand.ToUpper()))
            {
                if (Program.Character.AttributePoint > 0)
                {
                    LevelUp();
                }

                else
                {
                    Console.WriteLine("You don't have any attribute points.");
                }
            }

            else if (TranslationSystem.CommandFromFile.AddGold.Any(item => item == onlyCommand.ToUpper()))
            {
                Inventory.AddItem(new Gold(), GUI.GetNumberWithInput(1024, 0, onlyArgs));
                GUI.CommandActive();
            }

            else if (TranslationSystem.CommandFromFile.Quests.Any(item => item == onlyCommand.ToUpper()))
            {
                Quests(onlyArgs.ToUpper());
            }
        }

        public static void Quests(string arg)
        {
            switch (arg)
            {
                case "COMPLETED":
                    WriteCompletedQuest();
                    break;

                case "IN PROGRESS":
                    WriteInProgressQuest();
                    break;

                case "FAILED":
                    WriteFailedQuest();
                    break;

                default:
                    WriteInProgressQuest();
                    WriteCompletedQuest();
                    WriteFailedQuest();
                    break;
            }

            void WriteQuests(string text, IEnumerable<Quest> quests)
            {
                Console.Write($"Lista {text} \n");
                foreach (Quest item in quests)
                {
                    GUI.GenerateGUI('-', 120);
                    Console.WriteLine($"Nazwa: {item.Name}");
                    Console.WriteLine($"Opis: {item.Description}");
                    Console.WriteLine($"EXP: {item.EXPReward}");

                    Console.WriteLine("Przedmioty do zgarnięcia:");

                    foreach (InventoryRecord item2 in item.RewardItems)
                    {
                        Console.WriteLine($"- {item2.InventoryItem.Name}");
                    }

                    GUI.GenerateGUI('-', 120);
                }
            }

            void WriteCompletedQuest()
            {
                WriteQuests("zakończonych pomyślnie zadań:", QuestManager.CompletedQuests);
            }

            void WriteFailedQuest()
            {
                WriteQuests("spiepszonych zadań:", QuestManager.FailedQuests);
            }

            void WriteInProgressQuest()
            {
                WriteQuests("trwających zadań:", QuestManager.ActiveQuests);
            }
        }

        public static void LevelUp()
        {
            int[] tempAttributes = new int[] { Program.Character.Strength, Program.Character.Intelligence, Program.Character.Luck, Program.Character.Charisma };
            uint tempAttributePoints = Program.Character.AttributePoint;

            while (tempAttributePoints > 0)
            {
                Console.Clear();
                GUI.ColoredWriteLine(ConsoleColor.Green, $"{TranslationSystem.GUIFromFile["AttributesToSpend1"]} {tempAttributePoints} {TranslationSystem.GUIFromFile["AttributesToSpend2"]}");

                ControlEvent.SelectOptions
                (new Option($"{TranslationSystem.GUIFromFile["StrengthAttribute"]}: {Program.Character.Strength} (+{(Program.Character.Strength - tempAttributes[0]) / -1})", () => AddOnePoint(0)),
                 new Option($"{TranslationSystem.GUIFromFile["IntelligenceAttribute"]}: {Program.Character.Intelligence} (+{(Program.Character.Intelligence - tempAttributes[1]) / -1})", () => AddOnePoint(1)),
                 new Option($"{TranslationSystem.GUIFromFile["LuckAttribute"]}: {Program.Character.Luck} (+{(Program.Character.Luck - tempAttributes[2]) / -1})", () => AddOnePoint(2)),
                 new Option($"{TranslationSystem.GUIFromFile["CharismaAttribute"]}: {Program.Character.Charisma} (+{(Program.Character.Charisma - tempAttributes[3]) / -1})", () => AddOnePoint(3)));
            }

            if (tempAttributePoints == 0)
            {
                Console.Clear();
                Console.WriteLine($"\n {TranslationSystem.GUIFromFile["FinalStats"]}");
                GUI.GenerateGUI('-', 120);
                Console.WriteLine($"{TranslationSystem.GUIFromFile["StrengthAttribute"]}: {Program.Character.Strength} (+{(Program.Character.Strength - tempAttributes[0]) / -1})");
                Console.WriteLine($"{TranslationSystem.GUIFromFile["IntelligenceAttribute"]}: {Program.Character.Intelligence} (+{(Program.Character.Intelligence - tempAttributes[1]) / -1})");
                Console.WriteLine($"{TranslationSystem.GUIFromFile["LuckAttribute"]}: {Program.Character.Luck} (+{(Program.Character.Luck - tempAttributes[2]) / -1})");
                Console.WriteLine($"{TranslationSystem.GUIFromFile["CharismaAttribute"]}: {Program.Character.Charisma} (+{(Program.Character.Charisma - tempAttributes[3]) / -1})");
                GUI.GenerateGUI('-', 120);

                Console.WriteLine(TranslationSystem.GUIFromFile["AreYouSure"]);
                ControlEvent.SelectOptions
                    (
                    new Option(TranslationSystem.GUIFromFile["Yes"], () => FinalChoice(true)),
                    new Option(TranslationSystem.GUIFromFile["No"], () => FinalChoice(false))
                    );
            }


            void AddOnePoint(int attribute)
            {
                tempAttributes[attribute]++;
                tempAttributePoints--;
            }

            void FinalChoice(bool isTrue)
            {
                GUI.CommandActive();

                if (isTrue)
                {
                    Program.Character.SetAttributes(tempAttributes[0], tempAttributes[2], tempAttributes[1], tempAttributes[3]);
                }
            }
        }

        public static void Status(string arg)
        {
            Console.Clear();
            GUI.GenerateGUI('-', 120);

            switch (arg)
            {
                case "NAME":
                    Name(false);
                    break;

                case "POINTS":
                    Points(false);
                    break;

                case "ATTRIBUTES":
                    Attributes();
                    break;

                default:
                    Name(true);
                    Points(true);
                    Attributes();
                    break;
            }

            void Name(bool moreLines)
            {
                Console.WriteLine($"Name: {Program.Character.Name}");
                Console.WriteLine($"Class: {Program.Character.ClassName}");

                if (moreLines)
                {
                    Console.WriteLine($"Title: {Program.Character.ReputationStatus()} (Reputation: {Program.Character.Reputation}) \n \n");
                }
                else
                {
                    Console.WriteLine($"Title: {Program.Character.ReputationStatus()} (Reputation: {Program.Character.Reputation})");
                }
            }

            void Points(bool moreLines)
            {
                Console.WriteLine($"HP: {Program.Character.CurrentHP}/{Program.Character.MaxHP}");
                Console.WriteLine($"EXP: {Program.Character.CurrentEXP}/{Program.Character.MaxEXP}");
                Console.WriteLine($"Randomized ATT: {Program.Character.CalculusDamage()}");
                Console.WriteLine($"Randomized DEF: {Program.Character.CalculusDefense()}");

                if (moreLines)
                {
                    Console.WriteLine($"Randomized ESC: {Program.Character.CalculusEscape()} \n \n");
                }

                else
                {
                    Console.WriteLine($"Randomized ESC: {Program.Character.CalculusEscape()}");
                }
            }

            void Attributes()
            {
                Console.WriteLine($"STR: {Program.Character.Strength}");
                Console.WriteLine($"INT: {Program.Character.Intelligence}");
                Console.WriteLine($"LUCK: {Program.Character.Luck}");
                Console.WriteLine($"CHAR: {Program.Character.Charisma} \n");

                GUI.ColoredWriteLine(ConsoleColor.Green, $"You have {Program.Character.AttributePoint} attributes points to spend.");
            }

            GUI.GenerateGUI('-', 120);
        }

        public static void Help()
        {
            GUI.GenerateGUI('-', 120);

            Console.WriteLine("HELP:");
            Console.Write("-- INVENTORY ( ");
            TranslationSystem.CommandFromFile.Inventory.ForEach(command =>
            {
                Console.Write($"{command} ");
            });
            Console.Write(") \n");

            Console.Write("-- PAUSE ( ");
            TranslationSystem.CommandFromFile.Pause.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- BACK ( ");
            TranslationSystem.CommandFromFile.Back.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- NORTH ( ");
            TranslationSystem.CommandFromFile.GoNorth.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- SOUTH ( ");
            TranslationSystem.CommandFromFile.GoSouth.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- WEST ( ");
            TranslationSystem.CommandFromFile.GoWest.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- EAST ( ");
            TranslationSystem.CommandFromFile.GoEast.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- STATUS ( ");
            TranslationSystem.CommandFromFile.Status.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- LVLUP ( ");
            TranslationSystem.CommandFromFile.LevelUP.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            Console.Write("-- QUESTS ( ");
            TranslationSystem.CommandFromFile.Quests.ForEach(a =>
            {
                Console.Write($"{a} ");
            });
            Console.Write(") \n");

            GUI.GenerateGUI('-', 120);
        }

        public static void Movement()
        {
            foreach (IFollowPlayer followItem in
                   from item in SpawnRegionEntities.CurrentRegionEntities
                   where item is IFollowPlayer
                   select item as IFollowPlayer
                   )
            {
                followItem.Move();
            }

            Program.TypeCommand = true;
            CollisionHandler();
        }

        public static void InventoryOpen()
        {
            GUI.OpenedInventory = true;
            GUI.OpenedPauseMenu = false;
        }

        public static void Pause()
        {
            GUI.OpenedPauseMenu = true;
            GUI.OpenedInventory = false;
        }

        public static void Back()
        {
            GUI.CommandActive();
            GUI.OpenedPauseMenu = false;
            GUI.OpenedInventory = false;
        }

        /// <summary>
        /// Checks for collision in a place where player is.
        /// </summary>
        public static void CollisionHandler()
        {
            Program.NearbyEntities.Clear();

            // For each object with position in world.
            foreach (IPositionEntity item in SpawnRegionEntities.CurrentRegionEntities)
            {
                // If position of object == position of Character
                if (item.Position == Program.Character.Position)
                {
                    // Add to a nearby list.
                    Program.NearbyEntities.Add(item);

                    // Invoke method in Player with IPositionEntities.
                    Program.Character.Collision(Program.NearbyEntities.ToArray());
                }
            }

        }

    }
}
