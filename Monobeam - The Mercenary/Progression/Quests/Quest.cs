using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public abstract class Quest : IName
    {
        public Quest (Guid id, string name, string description, List<InventoryRecord> rewardItems, int expReward)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardItems = rewardItems;
            EXPReward = expReward;
        }

        public Guid ID { get; }
        public string Name { get; }

        public string Description { get; }

        public List<InventoryRecord> RewardItems { get; }
        public int EXPReward { get; }
    }
}
