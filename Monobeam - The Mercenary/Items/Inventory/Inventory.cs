using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class Inventory
    {
        public static event EventHandler<uint> OnItemPicked = delegate { };

        public static List<InventoryRecord> Items { get; set; } = new List<InventoryRecord>();

        private const int maxSlotsInventory = 9;

        public static uint ItemsPicked
        {
            get => _ItemsPicked;
            set
            {
                _ItemsPicked = value;
                OnItemPicked(null, value);
            }
        }

        private static uint _ItemsPicked;

        public static bool Exists<T>() where T : Item => Items.Any(item => item is T);
        public static bool Exists(Item item) => Items.Exists(itemRecord => (itemRecord.InventoryItem == item));

        #region Adding item

        public static void AddItem(Type type, int quantity) => AddItem(Activator.CreateInstance(type) as Item, quantity);

        public static void AddItem(InventoryRecord invRecord, int quantity) => AddItem(invRecord.InventoryItem, quantity);

        public static void AddItem<T>(int quantity) where T : Item, new() => AddItem(new T(), quantity);

        public static void AddItem(Item item, int quantity)
        {
            while (quantity > 0)
            {
                if (Items.Exists(itemRecord => (itemRecord.InventoryItem == item) && (itemRecord.Quantity < item.MaximumStackableQuantity)))
                {
                    ItemsPicked++;

                    InventoryRecord invRecord = Items.First(x => (x.InventoryItem == item) && (x.Quantity < item.MaximumStackableQuantity));

                    int maximumQuantityYouCanAddToThisStack = (item.MaximumStackableQuantity - invRecord.Quantity);

                    int quantityToAddToStack = Math.Min(quantity, maximumQuantityYouCanAddToThisStack);

                    invRecord.AddToQuantity(quantityToAddToStack);

                    quantity -= quantityToAddToStack;
                }

                else
                {
                    if (Items.Count < maxSlotsInventory)
                    {
                        Items.Add(new InventoryRecord(item, 0));
                    }

                    else
                    {
                        throw new Exception("There is no more space in the inventory");
                    }
                }
            }
        }
        #endregion

        #region Removing item
        public static void RemoveItemWithoutQuantity<T>() where T : Item => RemoveItemWithoutQuantity(typeof(T));

        public static void RemoveItemWithoutQuantity(Type type) => Items.Remove(Items.Find(inventoryRecord => inventoryRecord.InventoryItem.GetType() == type));

        public static void RemoveItemWithQuantity<T>(int quantity)
        {
            Item item = Items.Select(itemRecord => itemRecord.InventoryItem).Where(inventoryItem => inventoryItem is T).ToArray()[0];

            while (quantity > 0)
            {
                if (Items.Exists(itemRecord => (itemRecord.InventoryItem == item)))
                {
                    InventoryRecord invRecord = Items.First(x => (x.InventoryItem == item));

                    invRecord.RemoveFromQuantity(quantity);
                    quantity -= quantity;
                }
            }
        }

        public static void RemoveItemWithQuantity(Item item, int quantity)
        {
            Item itemSelected = Items.Select(itemRecord => itemRecord.InventoryItem).Where(inventoryItem => inventoryItem == item).ToArray()[0];

            while (quantity > 0)
            {
                if (Items.Exists(itemRecord => (itemRecord.InventoryItem == itemSelected)))
                {
                    InventoryRecord invRecord = Items.First(x => (x.InventoryItem == itemSelected));

                    invRecord.RemoveFromQuantity(quantity);
                    quantity -= quantity;
                }
            }
        }

        #endregion

        public static void Clear()
        {
            Items = new List<InventoryRecord>();
        }
    }
}
