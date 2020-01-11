using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class DustyDirtyHelmet : ArmorItem
    {
        public DustyDirtyHelmet() : base
            (
            name:"Stary, zbutchwiały hełm",
            id:Guid.NewGuid(),
            defMin:1,
            defMax:3
            )
        {

        }

        public override void Usage()
        {
            if (Program.Character != null)
            {
                Program.Character.Helmet = this;
            }
        }

        public override void Observe()
        {
            throw new NotImplementedException();
        }
    }
}
