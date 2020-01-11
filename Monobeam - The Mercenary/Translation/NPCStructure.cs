using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    public class NPCStructure
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Dialogue")]
        public Dictionary<int, string[]> Dialogue { get; set; } = new Dictionary<int, string[]>();

        public int CurrentDialogueLine;

        public string ChangeDialogue(int line, int line2 = 0)
        {
            CurrentDialogueLine = line;
            return Dialogue[CurrentDialogueLine][line2];
        }
    }
}
