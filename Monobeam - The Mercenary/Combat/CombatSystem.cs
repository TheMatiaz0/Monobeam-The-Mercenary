using System;
using System.Collections.Generic;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Audio;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Combat
{
    /// <summary>
    /// Class for all combat mechanics. To start use this method: "Start(IEntity[] entities)"
    /// </summary>
    public class CombatSystem
    {
        private static int ActualTurn;

        /// <summary>
        /// Count of enemies in active combat.
        /// </summary>
        private static int MaxTurn;

        /// <summary>
        /// Round is incrementing when enemies and player finish all turns and cycle goes again.
        /// </summary>
        private static uint ActualRound;

        /// <summary>
        /// Does combat is active right now?
        /// </summary>
        public static bool IsCombatActive;

        /// <summary>
        /// All IEntities, altogether with player (index: 0).
        /// </summary>
        public static readonly List<IEntity> TurnEntities = new List<IEntity>();

        private static readonly List<Option> Options = new List<Option>();

        /// <summary>
        /// Method to start the combat system.
        /// </summary>
        /// <param name="entities">Only enemies</param>
        public static void Start(IEntity[] entities)
        {
            AudioSystem.Play("157846__darkmast49__fightscene.wav", AudioChannel.Music, true);

            // Combat is active.
            IsCombatActive = true;

            // Add player to TurnEntities, make him/her index 0.
            TurnEntities.Add(Program.Character);

            // Add enemies (by parameter).
            for (int i = 0; i < entities.Length; i++)
            {
                IEntity item = entities[i];

                TurnEntities.Add(item);
            }

            // Set MaxTurn variable.
            MaxTurn = TurnEntities.Count() - 1;

            // Begin text:
            Console.WriteLine($"{TurnEntities[0].Name}, czy jesteś gotowy na walkę z...");

            // All entities without player:
            foreach (IEntity item in entities)
            {
                Console.WriteLine(item.Name);
            }

            // Wait for player to confirm.
            Console.ReadLine();

            // Randomize ActualTurn variable.
            ActualTurn = Program.Random.Next(MaxTurn);

            // Start this finally!
            Turns();
        }

        private static void Attack(IEntity target, int attacker)
        {
            switch (attacker)
            {
                case 0:
                    Console.WriteLine("Podchodzisz do przeciwnika.");
                    Console.WriteLine("Zaczynasz w szybkim tempie robić kołowrotek bronią.");
                    Console.Write("Ło, ło, ło. Udaje ci się zadać przeciwnikowi ");
                    break;

                default:
                    Console.Write($"{TurnEntities[attacker].Name} podchodzi do ciebie, miażdżąc twoją miękką czachę. Zabiera ci ");
                    break;
            }

            int dmgTaken = (int)TurnEntities[attacker].CalculusDamage() - (int)target.CalculusDefense();

            Console.ReadLine();

            if (dmgTaken <= 0)
            {
                GUI.ColoredWriteLine(ConsoleColor.Red, $"{Translation.TranslationSystem.GUIFromFile["0Attack"]} \n");

                dmgTaken = 0;
                Console.WriteLine($"{target.Name} {Translation.TranslationSystem.GUIFromFile["DodgeAttack"]}");
            }

            else
            {
                GUI.ColoredWriteLine(ConsoleColor.Red, $"{dmgTaken} {Translation.TranslationSystem.GUIFromFile["DamageTaken"]}");
            }


            target.CurrentHP -= dmgTaken;
            ZeroHPCheck();

            Console.ReadLine();

            MaxTurn = TurnEntities.Count() - 1;

            NextTurn();
        }

        private static void Completion()
        {
            Program.NearbyEntities.Clear();
            TurnEntities.Clear();
            Console.ReadLine();


            Program.TypeCommand = true;
            GUI.CommandActive();
        }

        private static void Turns()
        {
            Console.Clear();

            while (MaxTurn <= 0)
            {
                Completion();
            }

            for (int i = 0; i < TurnEntities.Count; i++)
            {
                IEntity item = TurnEntities[i];

                GUI.DrawTextProgressBar(item.CurrentHP, Convert.ToInt32(item.MaxHP), item.Name.ToUpper());

                Console.SetCursorPosition(12, 2 + (i * 2));
            }

            Console.WriteLine($"Round {ActualRound}");

            GUI.GenerateGUI('_', 120);

            if (ActualTurn == 0) // player turn
            {
                Console.WriteLine(Translation.TranslationSystem.GUIFromFile["YourTurn"]);

                ControlEvent.SelectOptions
                (new Option(Translation.TranslationSystem.GUIFromFile["Attack"], PlayerAttack),
                new Option(Translation.TranslationSystem.GUIFromFile["RestoreHP"], RestoreHP),
                 new Option(Translation.TranslationSystem.GUIFromFile["Escape"], () => Escape(Program.Character)));
            }


            if (ActualTurn != 0)
            {
                EnemyAttack(ActualTurn);
            }
        }

        private static void ZeroHPCheck()
        {
            foreach (IEntity item in TurnEntities)
            {
                if (item.CurrentHP < 1)
                {
                    item.Death();
                    RandomDrop((Enemy)item);
                    break;
                }
            }
        }

        private static void RandomDrop(Enemy enemy)
        {
            AudioSystem.Stop();

            int rndItem = Program.Random.Next(enemy.LootPackage.Count);

            InventoryRecord invRecord = enemy.LootPackage[rndItem];

            Inventory.AddItem(invRecord, invRecord.Quantity);

            GUI.ColoredWriteLine(ConsoleColor.Yellow, $"\n \n \n \n{Translation.TranslationSystem.GUIFromFile["WinCombat"]} {invRecord.InventoryItem.ToString()}");
            Console.ReadLine();

            Program.Character.AddEXP(Program.Random.Next(100, 300));
        }

        private static void RestoreHP()
        {
            GUI.DrawSlotsEat(Options);
            Options.Add(new Option(Translation.TranslationSystem.GUIFromFile["Back"], Turns));
            ControlEvent.SelectOptions(Options.ToArray());
            NextTurn();
        }

        private static void PlayerAttack()
        {
            Options.Clear();

            Console.WriteLine(Translation.TranslationSystem.GUIFromFile["ChooseEnemy"]);

            foreach (IEntity item in TurnEntities)
            {
                if (!item.Equals(Program.Character))
                {
                    Options.Add(new Option(item.Name, () => Attack(item, 0)));
                }

            }
            Options.Add(new Option(Translation.TranslationSystem.GUIFromFile["Back"], Turns));

            ControlEvent.SelectOptions(Options.ToArray());
        }

        private static void EnemyAttack(int enemyType)
        {
            Console.WriteLine($"{Translation.TranslationSystem.GUIFromFile["EnemyAttacks1"]} {TurnEntities[enemyType].Name} {Translation.TranslationSystem.GUIFromFile["EnemyAttacks2"]}");
            Console.WriteLine(TurnEntities[enemyType].Appearance);
            Console.ReadLine();

            Attack(Program.Character, enemyType);
            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadLine();

            NextTurn();
        }

        private static void Escape(IEntity entity)
        {
            float escapeChance = entity.CalculusEscape();
            Console.WriteLine($"{Translation.TranslationSystem.GUIFromFile["TryEscape"]} {escapeChance})");

            if (escapeChance < 50)
            {
                Console.WriteLine(Translation.TranslationSystem.GUIFromFile["EscapeFail"]);
                Console.ReadLine();

                NextTurn();
            }

            else
            {
                Console.WriteLine(Translation.TranslationSystem.GUIFromFile["EscapeSuccess"]);
                Console.ReadLine();

                Completion();
            }
        }

        private static void NextTurn()
        {
            if (MaxTurn > ActualTurn)
            {
                ActualTurn++;
            }

            else
            {
                ActualTurn = 0;
                ActualRound++;
            }

            Turns();
        }
    }
}
