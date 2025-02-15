using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    public class GamePattern
    {
        public int OffSetX { get; private set; } = 15;
        public int OffSetY { get; private set; } = 20;
        public bool BounceOnEdge { get; private set; } = true;

        public GamePattern(int offSetX, int offSetY, bool bounceOnEdge = true)
        {
            OffSetX = offSetX;
            OffSetY = offSetY;
            BounceOnEdge = bounceOnEdge;
        }

    }
}
