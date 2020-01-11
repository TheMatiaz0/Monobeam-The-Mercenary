using System;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;


namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public abstract class WeaponItem : Item
    {
        public int DmgMin { get; }
        public int DmgMax { get; }

        public WeaponItem(string name, Guid id, int dmgMax, int dmgMin, int priceToBuy = 0, int maximumStackableQuantity = 1) : base
            (
            name: name,
            id: id,
            itemAttribute: ItemAttribute.Weapon,
            maximumStackableQuantity: maximumStackableQuantity,
            priceToBuy: priceToBuy
            )
        {
            DmgMax = dmgMax;
            DmgMin = dmgMin;
        }

        public override void Eat()
        {
            Console.Clear();
            Console.WriteLine(Translation.TranslationSystem.ItemFromFile.WeaponItem.Eat[0]);
            GUI.ColoredWriteLine(ConsoleColor.DarkGray, Translation.TranslationSystem.GUIFromFile["Next"]);
            Console.ReadLine();
        }

        public override void Usage()
        {
            if (Program.Character != null)
            {
                Program.Character.WeaponEquipped = this;
            }

            else
            {
                Console.WriteLine("You can't equip it right now!");
            }
        }
    }
}

