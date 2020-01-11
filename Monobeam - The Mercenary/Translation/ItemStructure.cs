using Newtonsoft.Json;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    public class ItemStructure
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Observe")]
        public string[] Observe { get; set; }

        [JsonProperty("Eat")]
        public string[] Eat { get; set; }

        [JsonProperty("Usage")]
        public string[] Usage { get; set; }
    }
}
