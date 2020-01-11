using System;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public abstract class ArmorItem : Item
    {
        public int DefMin { get; }
        public int DefMax { get; }

        public ArmorItem(string name, Guid id, int defMin, int defMax, int priceToBuy = 0, int maximumStackable = 1) : base
            (
            name: name,
            id: id,
            itemAttribute: ItemAttribute.Armor,
            maximumStackableQuantity: maximumStackable,
            priceToBuy: priceToBuy
            )
        {
            DefMin = defMin;
            DefMax = defMax;
        }

        public override void Eat()
        {
            Console.WriteLine(Translation.TranslationSystem.ItemFromFile.ArmorItem.Eat[0]);
            Console.ReadLine();
        }
    }
}
