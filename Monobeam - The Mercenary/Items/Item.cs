using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheMatiaz0_MonobeamTheMercenary.Input;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public abstract class Item : IName
    {
        public Guid ID { get; set; }

        public string Name { get; }

        public int MaximumStackableQuantity { get; }

        public ItemAttribute ItemAttribute { get; set; } = ItemAttribute.None;

        public int PriceToBuy { get; } = 0;

        public virtual void Use()
        {
            Console.Clear();
            GUI.GenerateGUI('_', 120);
            Console.WriteLine($"What would you like to do with {Name} item?");

            ControlEvent.SelectOptions
            (new Option("Observe", Observe),
            new Option($"Use", Usage),
            new Option($"Eat", Eat));
            GUI.GenerateGUI('_', 120);
        }


        public Item (string name, Guid id, ItemAttribute itemAttribute, int priceToBuy = 0, int maximumStackableQuantity = 1)
        {
            Name = name;
            ID = id;
            ItemAttribute = itemAttribute;
            MaximumStackableQuantity = maximumStackableQuantity;
            PriceToBuy = priceToBuy;
        }

        /// <summary>
        /// Inspect/Observe a item, give a description of it.
        /// </summary>
        public abstract void Observe();

        /// <summary>
        /// For example - equip a item.
        /// </summary>
        public abstract void Usage();

        /// <summary>
        /// Only for Eatable items.
        /// </summary>
        public abstract void Eat();

        public static Type[] GetAllType (ItemAttribute attribute)
        {
            List<Type> results = new List<Type>();
            foreach (var item in Assembly.GetAssembly(typeof(Item)).GetTypes().Where(type => (type.IsSubclassOf(typeof(Item)) && type.IsAbstract == false)))
            {
                if ((Activator.CreateInstance(item) as Item).ItemAttribute.HasFlag(attribute))
                {
                    results.Add(item);
                }
            }
            return results.ToArray();
        }

        public static Type GetRandomItem(ItemAttribute attribute)
        {
            Type[] types = GetAllType(attribute);

            if (types.Length == 0)
            {
                throw new NotImplementedException("No item found with this attribute.");
            }

            int random = Program.Random.Next(0, types.Length);
            return types[random];

        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
