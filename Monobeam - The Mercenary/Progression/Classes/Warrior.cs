using System;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    /// <summary>
    /// Warrior class for Character.
    /// </summary>
    [Serializable]
    public class Warrior : Character
    {
        public Warrior(string nameVariable, Gender gender) : base
        (
            name: nameVariable,
            gender: gender,
            className: DialogueFromFile.Warrior.Name,

            hpMax: 200,

            luck: 5,
            strength: 11,
            intelligence: 3,
            charisma: 4
        )
        {
        }
    }
}
