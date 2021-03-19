using MeepoRunner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.GamePlay
{
    class Setup
    {
        public void Start(Util.HOMEBASE GamePlay)
        {   
            // Configue all the meepos positions (How can we calculate the first position of the meepo bar? ) 


            // Configure the game play
            if (GamePlay == Util.HOMEBASE.BOTTOM)
            {
                PositionMap.CURRENT_BASE = PositionMap.HOMEBASE_BOT;
            }
            if (GamePlay == Util.HOMEBASE.TOP)
            {
                PositionMap.CURRENT_BASE = PositionMap.HOMEBASE_TOP;
            }

        }
    }
}
