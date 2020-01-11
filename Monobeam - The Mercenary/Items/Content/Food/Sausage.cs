using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMatiaz0_MonobeamTheMercenary.Items
{
    public class Sausage : FoodItem
    {
        public Sausage() : base 
            (
            name:"Kiełbaska podwawelska",
            id:Guid.NewGuid(),
            addHP: 10
            )
        {
        }

        public override void Eat()
        {
            base.Eat();

            Console.WriteLine("Kurła, kiedyś to było. KURŁA JAKIE TO DOBRE!");
            Console.ReadLine();
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
