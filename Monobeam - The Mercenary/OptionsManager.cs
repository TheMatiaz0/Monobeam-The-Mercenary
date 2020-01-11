using Newtonsoft.Json;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class OptionsManager
    {
        public static OptionsManager Instance { get; set; }

        #region Gameplay
        [JsonProperty]
        public static string Language { get; set; } = "English";
        [JsonProperty]
        public static uint TypewriterEffectSpeed { get; set; } = 50;
        #endregion

        #region Audio
        [JsonProperty]
        public static bool BeepSound { get; set; } = true;
        [JsonProperty]
        public static uint Music { get; set; } = 100;
        [JsonProperty]
        public static uint Sounds { get; set; } = 100;
        #endregion
    }
}
