using System;
using System.Collections.Generic;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.Items;
using static TheMatiaz0_MonobeamTheMercenary.Translation.TranslationSystem;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public abstract class ShopkeeperNPC : TalkableNPC
    {
        private static readonly List<Option> Options = new List<Option>();

        public ShopkeeperNPC(string name, int charisma, List<InventoryRecord> itemsForSale, int maxGoldSell, ConsoleColor mainColor = ConsoleColor.Gray, int frequency = 203) : base
            (
            name: name,
            mainColor: mainColor,
            frequency: frequency,
            charisma: charisma
            )
        {
            ItemsForSale = itemsForSale;
            MaxGoldSell = maxGoldSell;
        }

        private static int Price;
        private static int Quantity;

        public List<InventoryRecord> ItemsForSale { get; } = new List<InventoryRecord>();

        public int MaxGoldSell { get; set; }

        public override void Talk()
        {
            Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(0), this);

            Options.Clear();

            Options.Add(new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(1, 0), PlayerBuy),
            new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(1, 1), PlayerSell));

            Options.Add(new Option(GUIFromFile["Back"], GUI.CommandActive));

            ControlEvent.SelectOptions(Options.ToArray());
        }

        public virtual void PaymentPlayer(InventoryRecord item, int price, int quantity)
        {
            if (Inventory.Items.Exists(item2 => item2.InventoryItem is Gold && item2.Quantity >= price))
            {
                Inventory.AddItem(item.InventoryItem, quantity);
                Inventory.RemoveItemWithQuantity<Gold>(price);
                ItemsForSale.Remove(item);

                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(2, 0), this);
                Console.WriteLine($"{DialogueFromFile.Shopkeeper.ChangeDialogue(2, 1)} {item.InventoryItem.Name} {DialogueFromFile.Shopkeeper.ChangeDialogue(2, 2)} {price}. {DialogueFromFile.Shopkeeper.ChangeDialogue(2, 3)}");
            }

            else
            {
                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(3, 0), Program.Character);
                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(3, 1), this);
                GUI.CommandActive();
            }
        }

        public virtual void PaymentNPC(InventoryRecord item, int price, int quantity)
        {
            if (MaxGoldSell >= Price)
            {
                MaxGoldSell -= Price;
                Inventory.RemoveItemWithQuantity(item.InventoryItem, quantity);
                ItemsForSale.Add(item);

                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(4, 0), Program.Character);
                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(4, 1), this);

                GUI.CommandActive();
            }

            else
            {
                Dialogue.Dialogue.PrintOutputText(DialogueFromFile.Shopkeeper.ChangeDialogue(5, 0), this);
                GUI.CommandActive();
            }
        }

        public virtual void ShopSpecificItem(InventoryRecord item, bool isSell)
        {
            Console.WriteLine(DialogueFromFile.Shopkeeper.ChangeDialogue(6, 0));
            Quantity = GUI.GetNumber(item.Quantity, 0);


            if (isSell)
            {
                Price = (item.InventoryItem.PriceToBuy / 2) * Quantity;

                Console.WriteLine($"{DialogueFromFile.Shopkeeper.ChangeDialogue(7, 0)} {item.InventoryItem.Name} {DialogueFromFile.Shopkeeper.ChangeDialogue(7, 1)} {Quantity}? {DialogueFromFile.Shopkeeper.ChangeDialogue(7, 2)} {Price}");

                ControlEvent.SelectOptions
                (
                new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(8, 0), () => SellDetermine(true)),
                new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(8, 1), () => SellDetermine(false))
                );
            }

            else
            {
                Price = (item.InventoryItem.PriceToBuy) * Quantity;

                Console.WriteLine($"{DialogueFromFile.Shopkeeper.ChangeDialogue(9, 0)} {item.InventoryItem.Name} {DialogueFromFile.Shopkeeper.ChangeDialogue(9, 1)} {Quantity}? {DialogueFromFile.Shopkeeper.ChangeDialogue(9, 2)} {Price}");

                ControlEvent.SelectOptions
                (
                new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(8, 0), () => BuyDetermine(true)),
                new Option(DialogueFromFile.Shopkeeper.ChangeDialogue(8, 1), () => BuyDetermine(false))
                );
            }


            #region Determine
            void SellDetermine(bool isTrue)
            {
                if (isTrue)
                {
                    PaymentNPC(item, Price, Quantity);
                }

                else
                {
                    GUI.CommandActive();
                }
            }

            void BuyDetermine(bool isTrue)
            {
                if (isTrue)
                {
                    PaymentPlayer(item, Price, Quantity);
                }

                else
                {
                    GUI.CommandActive();
                }
            }
        }
        #endregion

        public virtual void PlayerBuy()
        {
            int x = 0;
            Options.Clear();

            for (int i = 0; i < ItemsForSale.Count; i++)
            {
                Options.Add(new Option($"{ItemsForSale[i].InventoryItem.Name} x{ItemsForSale[i].Quantity} | Price (one): {ItemsForSale[i].InventoryItem.PriceToBuy}", () => ShopSpecificItem(ItemsForSale[x++], false)));
            }

            Options.Add(new Option(GUIFromFile["Back"], Talk));

            ControlEvent.SelectOptions(Options.ToArray());
        }

        public virtual void PlayerSell()
        {
            int x = 0;
            Options.Clear();

            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                Options.Add(new Option($"{Inventory.Items[i].InventoryItem.Name} x{Inventory.Items[i].Quantity} | Price (one): {(Inventory.Items[i].InventoryItem.PriceToBuy) / 2}", () => ShopSpecificItem(Inventory.Items[x++], true)));
            }

            Options.Add(new Option(GUIFromFile["Back"], Talk));

            ControlEvent.SelectOptions(Options.ToArray());
        }
    }
}
