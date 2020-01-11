using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.Progression
{
    public class Lucky : Character
    {
        public Lucky(string nameVariable, Gender gender) : base
        (
            name: nameVariable,
            gender: gender,
            className: DialogueFromFile.Lucky.Name,

            hpMax: 125,

            luck: 14,
            strength: 4,
            intelligence: 0,
            charisma: 2

        )

        { }
    }
}
