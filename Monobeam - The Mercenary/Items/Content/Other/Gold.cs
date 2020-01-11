using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class Gold : SpecialItem
    {
        public Gold() : base
            (
            name: "Gold",
            id: Guid.NewGuid(),
            itemAttribute: ItemAttribute.Money,
            priceToBuy: 0
            )
        { }
    }
}
