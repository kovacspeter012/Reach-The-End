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

        public int ID { get; private set; }

        public Section(List<GameGrid> starts, List<GameGrid> ends, List<GameGrid> elements, GamePattern gamePattern, int id)
        {
            Starts = starts;
            Ends = ends;
            Elements = elements;
            this.gamePattern = gamePattern;
            this.ID = id;
        }
        public Section(GamePattern gamePattern, int id)
        {
            this.gamePattern = gamePattern;
            this.ID = id;
        }
    }
}
