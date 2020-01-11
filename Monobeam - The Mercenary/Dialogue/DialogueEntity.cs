using System;

namespace TheMatiaz0_MonobeamTheMercenary.Dialogue
{
    /// <summary>
    /// Dialogue entity. Mainly for dialogues. Huh.
    /// </summary>
    public interface IDialogueEntity : IName
    {
        ConsoleColor MainColor { get; }

        int Frequency { get; }

        // Charisma gives you advantage in dialogues (ex. if you need special level of it).
        int Charisma { get; }
    }
}
