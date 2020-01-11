using System;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;
using System.Collections.Generic;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public class ZombieThomas : Enemy
    {
        public ZombieThomas() : base
        (
            appearance: "X",
            name: "Zombie Tomek",
            position: new Point(1, 0),
            enemyType: EnemyType.Zombie,
            hpMax: 5,
            strength: 5,
            luck: 10,
            weaponEquipped: new OldRustySword(),
            helmet: new DustyDirtyHelmet(),
            lootPackage: new List<InventoryRecord>() { new InventoryRecord(new Sausage(), 1)},
            expReward: 5
        )

        { }
    }
}
