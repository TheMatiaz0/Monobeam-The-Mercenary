
using System;
using System.Collections.Generic;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public class Zombieez : KillQuest
    {
        public Zombieez() : base
            (
            id: Guid.NewGuid(),
            name: "Zombieez!",
            description: "Kill 1 zombie.",
            rewardItems: new List<InventoryRecord>(new InventoryRecord[] { new InventoryRecord(new DustyDirtyHelmet(), 2) }),
            expReward: 100,
            targets: new List<Enemy>(new Enemy[] { new ZombieThomas() })
            )
        { }
    }
}

