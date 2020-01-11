using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    public class CommandStructure
    {
        [JsonProperty("Inventory")]
        public List<string> Inventory { get; set; } = new List<string>() { "INVENTORY" };

        [JsonProperty("Pause")]
        public List<string> Pause { get; set; } = new List<string>() { "PAUSE" };

        [JsonProperty("Back")]
        public List<string> Back { get; set; } = new List<string>() { "BACK" };

        [JsonProperty("GoNorth")]
        public List<string> GoNorth { get; set; } = new List<string>() { "NORTH" };

        [JsonProperty("GoSouth")]
        public List<string> GoSouth { get; set; } = new List<string>() { "SOUTH" };

        [JsonProperty("GoEast")]
        public List<string> GoEast { get; set; } = new List<string>() { "EAST" };

        [JsonProperty("GoWest")]
        public List<string> GoWest { get; set; } = new List<string>() { "WEST" };

        [JsonProperty("Help")]
        public List<string> Help { get; set; } = new List<string>() { "HELP" };

        [JsonProperty("Status")]
        public List<string> Status { get; set; } = new List<string>() { "STATUS" };

        [JsonProperty("LvlUP")]
        public List<string> LevelUP { get; set; } = new List<string>() { "LVLUP" };

        public List<string> AddGold { get; set; } = new List<string>() { "ADDGOLD" };

        public List<string> Quests { get; set; } = new List<string>() { "QUESTS" };
    }
}
