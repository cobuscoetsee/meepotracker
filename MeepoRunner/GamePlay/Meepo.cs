using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeepoRunner.GamePlay
{
    class Meepo
    {
        public Point position;
        public float health;
        public float manna;
        public Boolean isBleeding = false;

        
        public Meepo()
        {

        }

        public new string ToString => "Health:" + this.health + "Manna:" + this.manna + "Position:" + this.position.X + ";" + this.position.Y;

        public Boolean isActive()
        {

            return true;
        }
    }
}
