using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMatiaz0_MonobeamTheMercenary.Items;
using TheMatiaz0_MonobeamTheMercenary.Movement;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    public class PoniaczSeba : Enemy
    {
        public PoniaczSeba() : base 
            (
            appearance:@"
                                XXXXXXXXXXXXXXXXXXX
                             XXX                 XXXXXXXX
                           XXX                            XXXXXXX
                         XXXXXX                               XX
                        XX XX XXX                            XXX
                        X  X    XX                          XX
                       XX  X  X  XX                         X
                       X   X  XX  XX                        XXXXXX
                       X   X   X   XX  XXXXXXXXXXXXXXXXXXXXXX  XXXX
                       X   XX   XX  XXXX                   XXXXX XXX
                       X    XXX  X   X            X     XXXXXXXXX  X
                       X      XXX    X         XXXX        X   XX  XX
                       XX       XXXXXX  XXXXXXXXXXXXX      XXX  X   X
                        X            X      X      XX        XXXX   XX
                        X            XX     X     XX                 X
                        XX            X     XXXXXXX                 XX
                                      XX                           XX
                           XX          XX                          XXX
                            XX          X                            XXX
                             XXX        XX                             XX
                                XXXXX    XX                            XX
                                    XXXXXXX                           XX
                                          X                   XXXXXX XX
                                          X                    XXX  XX
                                          X                       XXX
                                     XXXXXX                         XXXXXXXXXXXXXXXXXXXX
                                 XXXX                              X     X             XXX
                         XXXXXXXXX                                XXXXXXXXXXXXXXXXX      XX
XXXXXXXXXXXXX        XXXX   XX                                   XX               XXX      XX
XXXXXXXX    XXXXXXXXXXXXX XXXXXXXXX                            XXX                  XX      XX
 XX    XXXXX    XXX XX            XXXXXX                       X                     X       X
  XXXXXXX   XXXXX   X    XXXX      XX   XXX                  XX                      XX      XX
         XXXXXXX    XXXX      XXXX        XXX              XXX                        XX      X
              XXXXX     XXXXXXXXX         XXX            XXX                           XX     X
                  XXXXXXXXX      XXX XXXXX X  XX      XXX                                XXXXXX
                         XXXXXXXXX   X    XX  X       X
                        XX XX    XX  X    X   X       X
                       XX  X     X  XX    X   X       X
                       X    XXXXXX  XXXXXXX  XXXXXXXXXXX
                      X         X   X     X  X        X
                      X        XX   X    X  XX       XX
                     X        XX   XX   XX  X        X
                     X       XX    X    X   X        X
                     XX    XX      X    X   X       X
                      XXXXXX       X   XX   XX     XX
                                   XXXXX     XXXXXXX
",
        name: "Poniacz Seba",
            position: new Point(20, 20),
            enemyType: EnemyType.Pony,
            hpMax: 5,
            strength:1,
            luck:5
            )   
        {
        }
    }
}

