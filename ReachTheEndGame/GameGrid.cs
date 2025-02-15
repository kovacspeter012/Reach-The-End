using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    public class GameGrid
    {
        public GameGrid(Rectangle rectangle, GameGridType gridType, bool isStart = false, bool isEnd = false)
        {
            Rectangle = rectangle;
            GridType = gridType;
            this.IsStart = isStart;
            this.IsEnd = isEnd;
        }

        public Rectangle Rectangle { get; private set; }
        public bool IsStart { get; private set; }
        public bool IsEnd { get; private set; }

        public GameGridType GridType { get; private set; }

        public Section Section { get; set; }
    }
    public enum GameGridType
    {
        Blank,
        Backwards,
        Double,
        Choose,
        GuessGame,
        MemoryGame,
        MoleGame,
        MineGame
    }
}
