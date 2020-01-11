using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public class Narrator : IDialogueEntity
    {
        public ConsoleColor MainColor => ConsoleColor.Green;

        public int Frequency => 253;

        public int Charisma => 0;

        public string Name => string.Empty;
    }
}
