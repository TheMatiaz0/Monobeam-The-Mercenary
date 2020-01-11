using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;
using TheMatiaz0_MonobeamTheMercenary.Translation;
using System.Diagnostics;
using System.Collections.Specialized;
using TheMatiaz0_MonobeamTheMercenary.Progression;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public abstract class Character : IDialogueEntity, IEntity, IEquipmentSlots, IPositionEntity
    {

        #region Constructors
        public Character(string name, Gender gender, string className, double hpMax, int luck, int strength, int intelligence, int charisma)
        {
            Name = name;
            Gender = gender;
            ClassName = className;

            MaxHP = hpMax;
            CurrentHP = Convert.ToInt32(MaxHP / 2);

            // Attributes:
            Luck = luck;
            Strength = strength;
            Intelligence = intelligence;
            Charisma = charisma;
        }

        public Character(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }

        #endregion


        #region Calculus
        public float CalculusDamage()
        {
            int randomizedNumber = Program.Random.Next((Luck - 10) / 2, Luck);
            int weaponDmg;

            if (WeaponEquipped != null)
            {
                weaponDmg = Program.Random.Next(WeaponEquipped.DmgMin, WeaponEquipped.DmgMax);
            }

            else
            {
                weaponDmg = 1;
            }

            return ((Strength - 10) / 2) + (randomizedNumber * 3) + weaponDmg;
        }

        public float CalculusEscape()
        {
            // double randomNumber = Program.Random.Next(0, Luck);
            double luckCalculated = Program.Random.Next(Luck, 100);

            return (float)luckCalculated;
        }

        public float CalculusDefense()
        {
            int[] armorPack = new int[] { 0, 0, 0, 0 };

            armorPack[0] = Program.Random.Next(Helmet?.DefMin ?? 0, Helmet?.DefMax ?? 0);
            armorPack[1] = Program.Random.Next(Chestplate?.DefMin ?? 0, Chestplate?.DefMax ?? 0);
            armorPack[2] = Program.Random.Next(Legging?.DefMin ?? 0, Legging?.DefMax ?? 0);
            armorPack[3] = Program.Random.Next(Boots?.DefMin ?? 0, Boots?.DefMax ?? 0);


            return (armorPack[0] + armorPack[1] + armorPack[2] + armorPack[3]) / 2;
        }

        public bool CalculusAnswer(int requiredCharismaPoints)
        {
            if (Charisma >= requiredCharismaPoints)
            {
                GUI.ColoredWriteLine(ConsoleColor.Green, $"Sukces! Twoja perswazja się udała (wymagane punkty: {requiredCharismaPoints}, twoja charyzma: {Charisma})");

                return true;
            }

            else
            {
                GUI.ColoredWriteLine(ConsoleColor.Red, $"Porażka! Twoja perswazja się nie udała (wymagane punkty: {requiredCharismaPoints}, twoja charyzma: {Charisma})");

                return false;
            }
        }

        #endregion

        #region Armor Pack

        public WeaponItem WeaponEquipped { get; set; }

        public ArmorItem Helmet { get; set; }

        public ArmorItem Chestplate { get; set; }

        public ArmorItem Legging { get; set; }

        public ArmorItem Boots { get; set; }

        #endregion

        #region EXP 

        #region Lvl

        public int CurrentLvl { get; private set; } = 1;

        public int MaxLvl { get; } = 30;

        private int realLvl = 1;

        #endregion

        public int CurrentEXP { get; set; } = 0;

        public double MaxEXP { get; set; } = 300;

        #endregion

        #region EXP Methods

        public void AddEXP(int add)
        {
            CurrentEXP += add;

            if (CurrentEXP >= MaxEXP)
            {
                realLvl++;
                CurrentEXP = 0;
            }

            Console.WriteLine($"{TranslationSystem.GUIFromFile["GainEXP1"]} {add} {TranslationSystem.GUIFromFile["GainEXP2"]}");
            Console.WriteLine($"{TranslationSystem.GUIFromFile["GainEXP3"]} {MaxEXP - CurrentEXP}");
            Console.WriteLine(ReloadLvl());
        }

        public string ReloadLvl()
        {
            string log = "";

            if (realLvl > CurrentLvl)
            {
                CurrentLvl = realLvl;

                MaxEXP *= 1.5f;
                MaxHP *= 1.25f;

                MaximizeHP();

                log = $"{TranslationSystem.GUIFromFile["LvlUP1"]} {CurrentLvl} {TranslationSystem.GUIFromFile["LvlUP2"]} {MaxHP}{TranslationSystem.GUIFromFile["LvlUP3"]}";
            }

            return log;
        }

        #endregion

        #region HP

        // Current hp is always half of max at the start of game.
        public int CurrentHP { get; set; }

        // Max hp can change depend on class.
        public double MaxHP { get; set; }

        public void AddCurrentHP(int add)
        {
            CurrentHP += add;

            if (CurrentHP > MaxHP)
            {
                MaximizeHP();
            }
        }

        public void MaximizeHP()
        {
            CurrentHP = Convert.ToInt32(MaxHP);
        }

        public void Death()
        {
            GameOver.IsGameOver = true;
        }

        #endregion

        #region Dialogue Part

        // Player name.
        public string Name { get; }

        public ConsoleColor MainColor { get; } = ConsoleColor.White;

        public int Frequency { get; } = 493;

        public int Charisma { get; set; }

        public Gender Gender { get; }

        #endregion

        #region Personality
        public int Empathy { get; set; } = 0;
        public int Poetic { get; set; } = 0;
        public int Independence { get; set; } = 0;
        #endregion


        public int Reputation { get; set; } = 41;

        public uint AttributePoint { get; set; } = 0;

        public void AddReputation (int howMany)
        {
            Reputation += howMany;

            Console.WriteLine($"Zmienia ci się reputacja. Masz teraz {Reputation} reputacji.");
        }

        public string ReputationStatus()
        {
            if (Reputation < 40)
            {
                return "Prawdziwy gangster z polotem i finezją w tym smutnym jak pizda świecie";
            }

            if (Reputation < 20)
            {
                return "Jeden z tych złoczyńców, co gada przez 4 godziny przed zabiciem arcywroga";
            }

            if (Reputation < 0)
            {
                return "Złodziej cukierków";
            }

            if (Reputation > 40)
            {
                return "Bohater, na którego wszyscy zasługujemy, ale nie ten, którego potrzebujemy w tym momencie.";
            }

            if (Reputation > 20)
            {
                return "Bohater osiedlowy";
            }

            if (Reputation > 0)
            {
                return "Pomocnik staruszek";
            }

            return "Wieśniak z Dzikiego Sadu";
        }

        public void SetAttributes (int strengthSet, int luckSet, int intelligenceSet, int charismaSet)
        {
            Strength = strengthSet;
            Luck = luckSet;
            Intelligence = intelligenceSet;
            Charisma = charismaSet;

            AttributePoint = 0;
        }

        // Class name.
        public string ClassName { get; }

        public string Appearance { get; } = null;

        #region Attributes
        // Strength is basically your attack power.
        public int Strength { get; set; }

        // Luck gives you a chance for more rare items.
        public int Luck { get; set; }

        // Inteligence gives you more EXP while playing.
        public int Intelligence { get; set; }

        #endregion

        public Point Position { get; set; } = new Point(0, 0);

        public void Collision(IPositionEntity[] collider)
        {
            foreach (IPositionEntity item in collider)
            {
                if (item.CollisionTag == CollisionTag.Enemy)
                {
                    IEntity[] entities = collider.Select(item2 => item2 as IEntity).ToArray();

                    CombatSystem.Start(entities);
                }

                else if (item.CollisionTag == CollisionTag.NPC)
                {
                    List<TalkableNPC> NPCEntities = collider.Select(item2 => item2 as TalkableNPC).ToList();

                    foreach (TalkableNPC item3 in NPCEntities)
                    {
                        item3.Talk();
                    }
                }
            }

        }

        public CollisionTag CollisionTag { get; } = CollisionTag.Player;
    }
}
