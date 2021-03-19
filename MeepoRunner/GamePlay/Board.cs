using MeepoRunner.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.GamePlay
{
    class Board
    {
        public static void Run()
        {
            // The second meepo will not be monitored as we want to leave this meepo at the base as soon as as the third meepo becomes available
            int count = 1;
            if (Monitor.MeepoCount() <= 2)
            {
                // Early game do nothing....
                return;
            }
            foreach (Point meepo in PositionMap.MEEPOS)
            {

                if (Monitor.Health(meepo) < 60 )
                {
                    Actions.Dig(meepo);
                } 
                if (Monitor.Health(meepo) < 30 )
                {
                    Actions.Blink(PositionMap.CURRENT_BASE);
                    Actions.RunToBase(meepo);
                }
                if (Monitor.Manna(meepo) < 20)
                {
                    Actions.Blink(PositionMap.CURRENT_BASE);
                    Actions.RunToBase(meepo);
                }
                Actions.Poof(meepo,meepo);
                count++;
            }
        }

    }
}
