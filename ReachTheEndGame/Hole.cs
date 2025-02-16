using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReachTheEndGame
{
    internal class Hole
    {
        public static string MoleImage { get; set; } = $"{Directory.GetCurrentDirectory()}\\Images\\HitTheMoleGame\\mole.png";
        public static string BombImage { get; set; } = $"{Directory.GetCurrentDirectory()}\\Images\\HitTheMoleGame\\bomb.png";
        public Image HoleElement { get; set; }
        public int Id { get; set; }
        public bool IsThereMole { get; set; }
        public bool IsThereBomb { get; set; }

        public Hole(Image holeElement, int id)
        {
            HoleElement = holeElement;
            Id = id;
            IsThereMole = false;
            IsThereBomb = false;
        }
    }
}
