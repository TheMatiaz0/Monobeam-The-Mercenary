using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public class TestQuest : Quest
    {
        public TestQuest() : base
            (
            id: Guid.NewGuid(),
            name: "Testowe zadanie",
            description: "Muszę to przecie jakoś przetestować",
            rewardItems: new List<InventoryRecord>() { new InventoryRecord(new Gold(), 250) },
            expReward: 20
            )
            { }
    }
}
