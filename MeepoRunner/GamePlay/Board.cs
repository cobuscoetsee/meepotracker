using MeepoRunner.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;

namespace MeepoRunner.GamePlay
{
    class Board
    {
        

        public static async Task Run(ILogger<Worker> logger)
        {

            int[] one = { 0x4F };
            
            //Util.PressKeyOnKeyboard(one);
            //return;
            // The second meepo will not be monitored as we want to leave this meepo at the base as soon as as the third meepo becomes available
            if (Monitor.MeepoCount <= 2)
            {
                // Early game do nothing....
                logger.LogInformation("Only limited meepos ", Monitor.MeepoCount);
                return;
            }

            await Actions.RunToBase(PositionMap.meepos[1]);
            // Calculate if in fight
            Boolean isBleeding = false;
            int inFight = 0;
            int count = 0;
            foreach (Meepo meepo in PositionMap.meepos)
            {
                if (meepo.isBleeding)
                {
                    isBleeding = true;
                    inFight++;
                }
                count++;
                if (count == 2) // Ensure the second meepo is in the base
                {
                    continue;
                }
            }
            Keyboard.SendInputWithAPI();


            count = 0;
            foreach (Meepo meepo in PositionMap.meepos)
            {
                count++;
                if (count == 2) // Ensure the second meepo is in the base
                {
                    continue;
                }
                if (isBleeding)
                {
                    if (meepo.manna < 20)
                    {
                        await Actions.Blink(meepo,PositionMap.CURRENT_BASE);
                        await Actions.RunToBase(meepo);
                    }
                    if (meepo.health < 60)
                    {
                        await Actions.Dig(meepo);
                    }
                    if (meepo.health < 30)
                    {
                        await Actions.Blink(meepo,PositionMap.CURRENT_BASE);
                        await Actions.RunToBase(meepo);
                        return;
                    }
                    Actions.Poof(meepo, Util.GetCursorPosition());
                }
            }
        }

    }
}
