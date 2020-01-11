using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    /// <summary>
    /// Bard class for Character.
    /// </summary>
    public class Bard : Character
    {
        public Bard(string nameVariable, Gender gender) : base
            (
                name: nameVariable,
                gender: gender,
                className: DialogueFromFile.Bard.Name,

                hpMax: 150,

                luck: 6,
                strength: 2,
                intelligence: 3,
                charisma: 13
            )
        { }
    }
}
