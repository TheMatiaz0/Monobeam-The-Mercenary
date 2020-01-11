using System;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class InventoryRecord
    {
        public Item InventoryItem { get; private set; }
        public int Quantity { get; private set; }

        public InventoryRecord(Item item, int quantity)
        {
            InventoryItem = item;
            Quantity = quantity;
        }

        public void AddToQuantity(int amountToAdd)
        {
            Quantity += amountToAdd;
        }

        public void RemoveFromQuantity(int amountToRemove)
        {
            Quantity -= amountToRemove;

            if (Quantity < 1)
            {
                Inventory.Items.Remove(this);
            }
        }
    }
}
