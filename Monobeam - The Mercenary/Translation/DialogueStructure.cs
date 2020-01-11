using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    public class DialogueStructure
    {
        [JsonProperty("Stranger")]
        public NPCStructure Stranger { get; set; } = new NPCStructure();

        [JsonProperty("Warrior")]
        public NPCStructure Warrior { get; set; } = new NPCStructure();

        [JsonProperty("Mage")]
        public NPCStructure Mage { get; set; } = new NPCStructure();

        [JsonProperty("Bard")]
        public NPCStructure Bard { get; set; } = new NPCStructure();

        [JsonProperty("Lucky")]
        public NPCStructure Lucky { get; set; } = new NPCStructure();

        [JsonProperty("Shopkeeper")]
        public NPCStructure Shopkeeper { get; set; } = new NPCStructure();
    }
}
