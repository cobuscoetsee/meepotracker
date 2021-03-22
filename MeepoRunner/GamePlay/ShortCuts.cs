using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.GamePlay
{
    class ShortCuts
    {
        //https://docs.microsoft.com/en-gb/windows/win32/inputdev/virtual-key-codes?redirectedfrom=MSDN
        public static int Q = 0x51; //DONE
        public static int W = 0x57; //DONE
        public static int E = 0x45; //DONE
        public static int D = 0x44; //DONE
        public static int R = 0x52; //DONE
        public static int Z = 0x5A; //DONE
        public static int X = 0x58; //DONE
        public static int C = 0x43; //DONE

        public static int ONE = 0x31; //Done




        public static int[] CTRL = new int[1]
                {
                    0x11
                };
        public static int[] SHIFT = new int[1]
                {
                    0x10
                };

        public static int TAB = 0x09; //DONE

        public static int[] DIG = new int[1]
                {
                    ShortCuts.D
                }; 
        public static int[] POOF = new int[1]
                {
                    ShortCuts.W
                };
        public static int[] NET = new int[1]
                {
                    ShortCuts.E
                };
        public static int[] SWITCH = new int[2]
                {
                    ShortCuts.CTRL[1],
                    ShortCuts.TAB
                };
        public static int[] BLINK = new int[1]
                {
                    ShortCuts.X
                };

        // WHAT WE CAN ADD LATER
        public static string CHANGE_GROUPING = "TAB";

    }
}

