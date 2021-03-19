using MeepoRunner.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.GamePlay
{
    class Actions
    {
        /*
         *  Will Poof meepo
         *  Need the ability to poof to specified point on minimap
         */
        public static void Poof(Point meepo, Point whereToOnMiniMap)
        {
            SelectMeepo(meepo);
            if (CanPoof())
            {
                // Do Poof Logic
                // Step 1 Move mouse cursor
                Util.SetCursorPos(whereToOnMiniMap.X, whereToOnMiniMap.Y);
                // Step 2 Press hotkey
                Util.PressKey(ShortCuts.POOF);
            } else
            {
                if (CanBlink())
                {
                    Blink(whereToOnMiniMap);
                    Dig(meepo);
                }
                Dig(meepo);
            }
        }

        /**
         * WIll select the first meepo and blink
         */
        public static void Blink(Point whereToOnMiniMap)
        {
            if (CanBlink())
            {
                //Do Logic to Blink 
                //Step 1 Move curser to Position to Blink To
                GoToPosition(whereToOnMiniMap);
                // Step 2 Press Shortcut (X)
                Util.PressKey(ShortCuts.BLINK);
            } else
            {
                //RunToBase(whereToOnMiniMap);
            }
        }

        private static void GoToPosition(Point position)
        {
            Util.SetCursorPos(position.X, position.Y);
            Util.DoMouseRightClick();
        }

        /**
         * Will tell the selected meepo to dig
         */
        public static void Dig(Point meepo, Boolean runToBase = false)
        {
            SelectMeepo(meepo);
            if (CanDig())
            {
                // Do Dig Logic
                Util.PressKey(ShortCuts.DIG);
            } else if (runToBase)
            {
                // run home 
                RunToBase(meepo);
            }
        }

        /**
         * Will take the meepo to the current defined base
         */
        public static void RunToBase(Point meepo)
        {
            SelectMeepo(meepo);
            // Do Logic To Run to Base
            GoToPosition(PositionMap.CURRENT_BASE);
            // Posibly remove from group selection
        }

        /**
         * Will try to highlight the selected meepo at the selected point
         */
        private static void SelectMeepo(Point meepo)
        {
            // Do logic to make sure the meepo you want stays selected by hitting Tab key we can swtich between meepos,
            // what might be quicker is to click on the meepo // Move cursor click might be distubring however if we can move curser back to original place it might be good
            Util.SetCursorPos(meepo.X, meepo.Y);
            Util.DoMouseLeftClick();
        }

        private static Boolean CanPoof()
        {
            // Do logic
            return true;
        }
        private static Boolean CanDig()
        {
            // Do Logic
            return true;
        }
        private static Boolean CanBlink()
        {
            // Do Logic 
            return true;
        }

    }
}
