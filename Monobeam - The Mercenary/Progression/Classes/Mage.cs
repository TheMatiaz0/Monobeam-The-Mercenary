using System;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    [Serializable]
    public class Mage : Character
    {
        public Mage(string name, Gender gender) : base
        (
            name: name,
            gender: gender,
            className: DialogueFromFile.Mage.Name,

            hpMax: 150,
            luck: 7,
            strength: 3,
            intelligence: 11,
            charisma: 4
        )
        {
        }
    }
}
