using System;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public class PauseMenuButtons
    {
        public static void Resume()
        {
            Console.Clear();
            GUI.OpenedPauseMenu = false;
            GUI.OpenedInventory = false;
            return;
        }

    }
}
