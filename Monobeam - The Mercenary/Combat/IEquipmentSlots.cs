using TheMatiaz0_MonobeamTheMercenary.Items;

namespace TheMatiaz0_MonobeamTheMercenary.Combat
{
    /// <summary>
    /// Use it, when entity uses armor/weapon.
    /// </summary>
    public interface IEquipmentSlots
    {
        WeaponItem WeaponEquipped { get; }

        #region Armor

        ArmorItem Helmet { get; set; }

        ArmorItem Chestplate { get; set; }

        ArmorItem Legging { get; set; }

        ArmorItem Boots { get; set; }

        #endregion
    }
}
