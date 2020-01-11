using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public abstract class SpecialItem : Item
    {
        public SpecialItem(string name, Guid id, ItemAttribute itemAttribute, int priceToBuy = 0) : base
            (
            name: name,
            id:id,
            itemAttribute: itemAttribute,
            priceToBuy: priceToBuy,
            maximumStackableQuantity: 1024
            )
        { }

        public override void Eat()
        {
            throw new NotImplementedException();
        }

        public override void Observe()
        {
            throw new NotImplementedException();
        }

        public override void Usage()
        {
            throw new NotImplementedException();
        }
    }
}
