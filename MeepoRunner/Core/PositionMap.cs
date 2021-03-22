using MeepoRunner.GamePlay;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        

        // Defining Meepo 
            
        // Defining Home Base
        public static Point HOMEBASE_BOT = new Point { X = 231, Y = 849 };
        public static Point HOMEBASE_TOP = new Point { X = 15, Y = 1060 };

        public static Point CURRENT_BASE = new Point { X = 0, Y = 0 };

        public static Meepo[] meepos = new Meepo[5] {
                new Meepo(),
                new Meepo(),
                new Meepo(),
                new Meepo(),
                new Meepo()
            };

        public static void RecalculateMeepos(ILogger<Worker> logger)
        {
            Bitmap healthbar = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\resources\\img\\healthbar.png");

            Bitmap screenshot  = Util.Screenshot();
            screenshot.Save("c:\\Source\\dota\\logs\\screenshot.png", ImageFormat.Png);


            int meepoCounter = 0;
            Meepo meepo = new Meepo();
            for (int x = 0; x < screenshot.Height; x+=1)
            {
                int imageStart = 0;
                if (x > 511) break; 
                for (int y = 0; y < screenshot.Width; y+=1)
                {
                    if (y > 112) break;
                    Boolean meepoFound = false;
                    for (int hbw = 0; hbw < healthbar.Height; hbw++)
                    {
                        Boolean matchPixel = false;
                        int health = 3;
                        int manna = 10;
                        int matched = 0;
                        for (int hbh = 0; hbh < healthbar.Width; hbh++)
                        {

                            Color currentColor = Util.GetColorAtOnImage(screenshot,  y+ hbh, x + hbw);
                            Color healthbarColor = Util.GetColorAtOnImage(healthbar,  hbh, hbw);
                            if (healthbarColor.Equals(currentColor))
                            {
                                matched++;
                                meepoFound = true;
                                
                                matchPixel = true;
                                if (imageStart==0)
                                {
                                    imageStart = x;
                                }
                                //
                                //logger.LogDebug("{meepoCounter} ::: {x} {y} = {z} {imageStart}", meepoCounter, x +hbw, y + hbh, healthbar, imageStart);
                            } else
                            {
                                break;
                            }
                        }
                        double percentage = (double)matched / healthbar.Width;
                        if (hbw == health) {
                            meepo.health = (float) percentage * 100f;
                            meepo.position = new Point(y+3, x);
                        }
                        if (hbw == manna)
                        {
                            meepo.manna = (float) percentage * 100f;
                        }


                        if (matchPixel== false)
                        {
                            break;
                        }
                    }
                    if (meepoFound)
                    {
                        meepos[meepoCounter] = meepo;
                        meepoCounter += 1;
                        logger.LogDebug("{meepoCounter} ::: {meepo} =  {imageStart}", meepoCounter, meepo.ToString, imageStart);
                        
                        meepo = new Meepo();
                        x += 20;
                        break;
                    }
                }
                
            }
            Monitor.MeepoCount = meepoCounter+1;

            Log.CloseAndFlush();
        }

    }
}
