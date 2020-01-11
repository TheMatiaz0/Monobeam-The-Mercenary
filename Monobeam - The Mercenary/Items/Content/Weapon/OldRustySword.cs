using System;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class OldRustySword : WeaponItem
    {
        public OldRustySword() : base
            (
            name: Translation.TranslationSystem.ItemFromFile.OldRustySword.Name,
            id: Guid.NewGuid(),
            dmgMin: 2,
            dmgMax: 4,
            maximumStackableQuantity: 3,
            priceToBuy: 10
            )
        {
        }

        public override void Observe()
        {
            Console.WriteLine($"{Translation.TranslationSystem.ItemFromFile.OldRustySword.Observe[0]} {DmgMin}-{DmgMax} {Translation.TranslationSystem.ItemFromFile.OldRustySword.Observe[1]}");
            Console.ReadLine();
        }
    }
}
