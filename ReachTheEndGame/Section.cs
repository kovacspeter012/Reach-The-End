using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    public class Section
    {
        public List<GameGrid> Starts = new();
        public List<GameGrid> Ends = new();

        public List<GameGrid> Elements = new();

        public GamePattern gamePattern;

        public Section(List<GameGrid> starts, List<GameGrid> ends, List<GameGrid> elements, GamePattern gamePattern)
        {
            Starts = starts;
            Ends = ends;
            Elements = elements;
            this.gamePattern = gamePattern;
        }
        public Section(GamePattern gamePattern)
        {
            this.gamePattern = gamePattern;
        }
        public Section() { }
    }
}
