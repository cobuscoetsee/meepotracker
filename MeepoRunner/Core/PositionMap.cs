using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.Core
{
    class PositionMap
    {
        // Defining Meepos
        public static Point MEEPO1 = new Point { X = 0, Y = 0 };
        public static Point MEEPO2 = new Point { X = 0, Y = 0 };
        public static Point MEEPO3 = new Point { X = 0, Y = 0 };
        public static Point MEEPO4 = new Point { X = 0, Y = 0 };
        public static Point MEEPO5 = new Point { X = 0, Y = 0 };

        // Defining Meepo arrays
        public static Point[] MEEPOS = new Point[5] {
            MEEPO1,
            MEEPO2,
            MEEPO3,
            MEEPO4,
            MEEPO5,
        };

        // Defining Meepo 

        // Defining Home Base
        public static Point HOMEBASE_BOT = new Point { X = 0, Y = 0 };
        public static Point HOMEBASE_TOP = new Point { X = 0, Y = 0 };

        public static Point CURRENT_BASE = new Point { X = 0, Y = 0 };

    }
}
