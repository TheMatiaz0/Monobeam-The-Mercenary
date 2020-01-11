
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public abstract class GatherQuest : Quest
    {
        public GatherQuest(Guid id, string name, string description, List<InventoryRecord> rewardItems, int expReward, List<InventoryRecord> itemsToGather) : base
            (
                id: id,
                name: name,
                description: description,
                rewardItems: rewardItems,
                expReward: expReward
            )
        {
            ItemsToGather = itemsToGather;
        }

        public List<InventoryRecord> ItemsToGather { get; }
    }
}