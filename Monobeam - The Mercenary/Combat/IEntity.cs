namespace TheMatiaz0_MonobeamTheMercenary.Combat
{
    public interface IEntity : IName
    {
        string Appearance { get; }

        /// <summary>
        /// Calculate damage that entity takes...
        /// </summary>
        /// <returns>DMG float</returns>
        float CalculusDamage();

        /// <summary>
        /// Calculate entity's chance for escape. 
        /// </summary>
        /// <returns>DMG float</returns>
        float CalculusEscape();

        /// <summary>
        /// Calculate ability to defense from taken DMG.
        /// </summary>
        /// <returns></returns>
        float CalculusDefense();

        int CurrentHP { get; set; }

        double MaxHP { get; set; }

        // Strength is basically your attack power.
        int Strength { get; }

        // Luck gives you a chance for more rare items.
        int Luck { get; }

        void Death();

    }
}
