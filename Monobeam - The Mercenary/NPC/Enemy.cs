using System;
using System.Collections.Generic;
using System.Diagnostics;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public abstract class Enemy : IEntity, IPositionEntity, IEquipmentSlots, IFollowPlayer
    {
        public Enemy(string name, Point position, EnemyType enemyType, int hpMax, string appearance, int strength,
            int luck, int expReward = 0, List<InventoryRecord> lootPackage = null, WeaponItem weaponEquipped = null,
            ArmorItem helmet = null, ArmorItem chestplate = null, ArmorItem legging = null, ArmorItem boots = null)
        {
            Name = name;
            EnemyType = enemyType;
            Position = position;

            MaxHP = hpMax;
            CurrentHP = Convert.ToInt32(MaxHP);

            Appearance = appearance;

            Strength = strength;
            Luck = luck;

            WeaponEquipped = weaponEquipped;
            Helmet = helmet;
            Chestplate = chestplate;
            Legging = legging;
            Boots = boots;

            LootPackage = lootPackage;
            EXPReward = expReward;
        }

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
            int luckCalculated = (Luck - 10) / 2;
            float randomNumber = (float)Program.Random.NextDouble();

            return 10 + (luckCalculated * 3) * randomNumber;
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

        #endregion

        public void Death()
        {
            CombatSystem.TurnEntities.Remove(this);
            SpawnRegionEntities.RemoveEntity(this);
            Program.NearbyEntities.Remove(this);

            Program.EnemiesKilled.Add(this);
        }

        public void Calculate()
        {
            DiffX = Program.Character.Position.X - Position.X;
            DiffY = Program.Character.Position.Y - Position.Y;
        }

        public void Move()
        {
            Calculate();

            if (DiffX < SightFar || DiffY < SightFar)
            {
                // Lower than zero, that means "-" (Left)
                if (DiffX < 0)
                {
                    Position = new Point(Position.X - MovementSpeed, Position.Y);
                }

                // Higher than zero, that means "+" (Right)
                else if (DiffX > 0)
                {
                    Position = new Point(Position.X + MovementSpeed, Position.Y);
                }

                // DOWN (-)
                if (DiffY < 0)
                {
                    Position = new Point(Position.X, Position.Y - MovementSpeed);
                }

                // UP (+)
                else if (DiffY > 0)
                {
                    Position = new Point(Position.X, Position.Y + MovementSpeed);
                }


                Debug.WriteLine($"X: {Position.X}, Y: {Position.Y}");
            }
        }

        public string Name { get; }

        public EnemyType EnemyType { get; }

        #region HP

        public int CurrentHP { get; set; }
        public double MaxHP { get; set; }

        #endregion

        public int DmgMin { get; }

        public int DmgMax { get; }

        public List<InventoryRecord> LootPackage { get; }

        public int EXPReward { get; }

        public string Appearance { get; }

        public Point Position { get; set; }

        public CollisionTag CollisionTag { get; } = CollisionTag.Enemy;

        public int Strength { get; }

        public int Luck { get; }

        #region Armor Pack
        public WeaponItem WeaponEquipped { get; set; }
        public ArmorItem Helmet { get; set; }
        public ArmorItem Chestplate { get; set; }
        public ArmorItem Legging { get; set; }
        public ArmorItem Boots { get; set; }

        public int DiffX { get; set; }

        public int DiffY { get; set; }

        public int SightFar { get; }

        public int MovementSpeed { get; } = 1;
        #endregion
    }
}
