
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Combat;
using TheMatiaz0_MonobeamTheMercenary.NPC;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public abstract class KillQuest : Quest
    {
        public KillQuest(Guid id, string name, string description, List<InventoryRecord> rewardItems, int expReward, List<Enemy> targets) : base
            (
            id: id,
            name: name,
            description: description,
            rewardItems: rewardItems,
            expReward: expReward
            )
        {
            Targets = targets;
        }

        public List<Enemy> Targets { get; }
    }
}

