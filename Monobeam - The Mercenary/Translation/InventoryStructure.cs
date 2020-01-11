using Newtonsoft.Json;

namespace TheMatiaz0_MonobeamTheMercenary.Translation
{
    public class InventoryStructure
    {
        [JsonProperty("OldRustySword")]
        public ItemStructure OldRustySword { get; set; } = new ItemStructure();

        [JsonProperty("WeaponItem")]
        public ItemStructure WeaponItem { get; set; } = new ItemStructure();

        [JsonProperty("ArmorItem")]
        public ItemStructure ArmorItem { get; set; } = new ItemStructure();
    }
}
