using System;
using System.Collections.Generic;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Dialogue;
using TheMatiaz0_MonobeamTheMercenary.Input;
using System.Diagnostics;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public class BlacksmithRoger : ShopkeeperNPC
    {
        public BlacksmithRoger() : base
        (
            name: "Kowal Rodżer",
            mainColor: ConsoleColor.Yellow,
            frequency: 203,
            charisma: 0,
            itemsForSale: new List<InventoryRecord>(new InventoryRecord[] { new InventoryRecord(new OldRustySword(), 2) }),
            maxGoldSell: 20
        )
        { }

        private static bool FirstTalk = false;

        public override void Talk()
        {
            if (FirstTalk)
            {
                Dialogue.Dialogue.PrintOutputText("Zaraz, chwila. Ja cię chyba skądś kojarzę. Hmm...", this);

                ControlEvent.SelectOptions
                    (
                new Option("(WPŁYW: CHARYZMA) Nah, niemożliwe. Musiałeś sobie coś ubzdurać.", () => Persuasion(Program.Character.CalculusAnswer(4))),
                    new Option("No tak, byłem tu poprzednio. Masz pamięć jak drewno.", Confirmation)             
                    );

                void Persuasion (bool isSuccess)
                {
                    if (isSuccess)
                    {
                        Dialogue.Dialogue.PrintOutputText("Może faktycznie masz rację. Kurde no, łeb mie boli od tego wszystkiego.", this);
                        Dialogue.Dialogue.PrintOutputText("Już powoli tracę rachubę, co jest prawdą, a co kłamstwem.", this);
                    }

                    else
                    {
                        Program.Character.Reputation -= 2;
                        Dialogue.Dialogue.PrintOutputText("Pierdolisz. Byłeś tu na pewno. Kupowałeś u mnie coś, chyba, albo... sprzedawałeś może? W każdym razie, byłeś tu z pewnością.", this);
                        Dialogue.Dialogue.PrintOutputText("Może masz rację. Wybacz, mój błąd.", Program.Character);
                    }
                }

                void Confirmation ()
                {
                    Dialogue.Dialogue.PrintOutputText("Ty, rzeczywiście. Masz rację.", this);
                }
            }

            FirstTalk = true;
            base.Talk();
        }
    }
}
