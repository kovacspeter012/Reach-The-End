using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ReachTheEndGame
{
    internal class Hole
    {
        private bool isThereMole;
        private bool isThereBomb;
        public static string MoleImage = $"{Directory.GetCurrentDirectory()}\\Images\\HitTheMoleGame\\mole.png";
        public static string BombImage = $"{Directory.GetCurrentDirectory()}\\Images\\HitTheMoleGame\\bomb.png";
        public Image HoleElement { get; set; }
        public int Id { get; set; }
        public bool IsThereMole
        {
            get
            {
                return isThereMole;
            }
            set
            {
                if (value == true)
                {
                    HoleElement.Source = new BitmapImage(new Uri(MoleImage));
                }
                else
                {
                    HoleElement.Source = null;
                }
                isThereMole = value;
            }
        }
        public bool IsThereBomb
        {
            get
            {
                return isThereBomb;
            }
            set
            {
                if (value == true)
                {
                    HoleElement.Source = new BitmapImage(new Uri(BombImage));
                }
                else
                {
                    HoleElement.Source = null;
                }
                isThereBomb = value;
            }
        }

        public Hole(Image holeElement, int id)
        {
            HoleElement = holeElement;
            Id = id;
            IsThereMole = false;
            IsThereBomb = false;
        }
    }
}
