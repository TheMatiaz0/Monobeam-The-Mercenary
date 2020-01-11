using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public abstract class FoodItem : Item
    {
        public FoodItem (string name, Guid id, int addHP) : base 
            (
            name:name,
            id:id,
            itemAttribute:ItemAttribute.Food,
            maximumStackableQuantity:16
            )
        {
            AddHP = addHP;
        }

        public int AddHP { get; }

        public override void Eat()
        {
            Inventory.RemoveItemWithQuantity<Sausage>(1);
            Program.Character.AddCurrentHP(AddHP);
        }

    }
}
