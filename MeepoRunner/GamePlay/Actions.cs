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
        public static async void Poof(Meepo meepo, Point whereToOnMiniMap)
        {
            await SelectMeepo(meepo);
            if (CanPoof())
            {
                Point currentPosition = Util.GetCursorPosition();
                // Do Poof Logic
                // Step 1 Move mouse cursor
                await Util.SetCursorTo(whereToOnMiniMap.X, whereToOnMiniMap.Y);
                // Step 2 Press hotkey
                await Util.PressKeyOnKeyboard(ShortCuts.POOF);
                await Util.SetCursorTo(currentPosition.X, currentPosition.Y);
                await Util.DoMouseLeftClickAsync(currentPosition);

            }
            else
            {
                if (CanBlink(meepo))
                {
                    await Blink(meepo,whereToOnMiniMap);
                    await Dig(meepo);
                }
                await Dig(meepo);
            }
        }

        /**
         * WIll select the first meepo and blink
         */
        public static async Task Blink(Meepo meepo, Point whereToOnMiniMap)
        {
            if (CanBlink(meepo))
            {
                await SelectMeepo(PositionMap.meepos[0]);
                //Do Logic to Blink 
                //Step 1 Move curser to Position to Blink To
                await GoToPosition(whereToOnMiniMap);
                // Step 2 Press Shortcut (X)
                await Util.PressKeyOnKeyboard(ShortCuts.BLINK);
            } else
            {
                //RunToBase(whereToOnMiniMap);
            }
        }

        private static async Task GoToPosition(Point position)
        {
            Point currentPosition = Util.GetCursorPosition();

            await Util.SetCursorTo(position.X,position.Y);
            await Util.DoMouseRightClick(position);
            await Util.SetCursorTo(currentPosition.X, currentPosition.Y);
            await Task.Delay(500);
            await Util.DoMouseLeftClickAsync(currentPosition);

        }

        /**
         * Will tell the selected meepo to dig
         */
        public static async Task Dig(Meepo meepo, Boolean runToBase = false)
        {
            await SelectMeepo(meepo);
            if (CanDig())
            {
                // Do Dig Logic
                await Util.PressKeyOnKeyboard(ShortCuts.DIG);
            } else if (runToBase)
            {
                // run home 
                await RunToBase(meepo);
            }
        }

        /**
         * Will take the meepo to the current defined base
         */
        public static async Task RunToBase(Meepo meepo)
        {
            await SelectMeepo(meepo);
            // Do Logic To Run to Base
            await GoToPosition(PositionMap.CURRENT_BASE);
            // Posibly remove from group selection

        }
        /**
        * Will try to highlight the selected meepo at the selected point
        */
        public static async Task SelectMeepo(Meepo meepo, Boolean add)
        {
            // Do logic to make sure the meepo you want stays selected by hitting Tab key we can swtich between meepos,
            // what might be quicker is to click on the meepo // Move cursor click might be distubring however if we can move curser back to original place it might be good
            Point currentPosition = Util.GetCursorPosition();
            await Util.SetCursorTo(meepo.position.X, meepo.position.Y);
            if (add)
            {
                Util.HoldKey(ShortCuts.SHIFT);

            }
            await Util.DoMouseLeftClickAsync(meepo.position);
            if (add)
            {
                Util.ReleaseKey(ShortCuts.SHIFT);

            }
            await Util.SetCursorTo(currentPosition.X, currentPosition.Y);
            await Util.DoMouseLeftClickAsync(currentPosition);

        }
        /**
         * Will try to highlight the selected meepo at the selected point
         */
        public static async Task SelectMeepo(Meepo meepo)
        {
            await SelectMeepo(meepo, false);
        }

        private static Boolean CanPoof()
        {
            // Do logic
            return false;
        }
        private static Boolean CanDig()
        {
            // Do Logic
            return false;
        }
        private static Boolean CanBlink(Meepo meepo)
        {
            if (PositionMap.meepos[0].Equals(meepo))
            {
                return true;
            }
            return false;
        }

    }
}
