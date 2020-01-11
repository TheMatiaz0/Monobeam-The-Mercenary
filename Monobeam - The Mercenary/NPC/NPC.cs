using System;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.Movement;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public abstract class TalkableNPC : IDialogueEntity, IPositionEntity
    {
        public TalkableNPC(string name, ConsoleColor mainColor, int frequency, int charisma)
        {
            Name = name;

            MainColor = mainColor;
            Frequency = frequency;
            Charisma = charisma;
        }

        public string Name { get; }

        public ConsoleColor MainColor { get; }

        public int Frequency { get; }

        public int Charisma { get; }

        public Point Position { get; set; }

        public CollisionTag CollisionTag { get; } = CollisionTag.NPC;

        public abstract void Talk();
    }
}
