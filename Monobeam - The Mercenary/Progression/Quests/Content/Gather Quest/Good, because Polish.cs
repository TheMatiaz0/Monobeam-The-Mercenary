
using System;
using System.Collections.Generic;
using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public class Good__because_Polish : GatherQuest
    {
        public Good__because_Polish() : base
            (
            id: Guid.NewGuid(),
            name: "Dobreeee, bo polskie.",
            description: "Zbierz 2 kiełbaski podwawelskie.",
            rewardItems: new List<InventoryRecord>(new InventoryRecord[] { new InventoryRecord(new Sausage(), 2) }),
            expReward: 300,
            itemsToGather: new List<InventoryRecord>(new InventoryRecord[] { new InventoryRecord(new Sausage(), 2)})
            )
        { }
    }
}