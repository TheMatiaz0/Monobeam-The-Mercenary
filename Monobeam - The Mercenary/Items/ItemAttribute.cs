using System;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    [Flags]
    public enum ItemAttribute
    {
        None = 0,
        Food = 1 << 0,
        Weapon = 1 << 1,
        Armor = 1 << 2,
        Potion = 1 << 3,
        Readable = 1 << 4,
        Quest = 1 << 5,
        Money = 1 << 6
    }
}
