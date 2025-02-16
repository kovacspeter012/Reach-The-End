using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for HitTheMoleWindow.xaml
    /// </summary>
    public partial class HitTheMoleWindow : Window
    {
        private int timeLeft = 30;
        private List<Hole> holes = new List<Hole>();
        Random random = new Random();
        public HitTheMoleWindow()
        {
            InitializeComponent();
            DispatcherTimer aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += (sender, e) =>
            {
                resetHoles();
                timeLeft -= 1;
                lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
                int randomHoleIndex = random.Next(0, 7);
                holes[randomHoleIndex].IsThereMole = true;

                if (timeLeft < 1)
                {
                    aTimer.Stop();
                }
            };
            aTimer.Start();

            Loaded += (sender, e) =>
            {
                CreateHoles();
                foreach (Hole hole in holes)
                {
                    hole.HoleElement.MouseLeftButtonDown += (sender, e) =>
                    {
                        if (hole.IsThereMole)
                        {
                            
                        }
                        else if (hole.IsThereBomb)
                        {
                            
                        }
                    };
                }
            };
        }

        private void resetHoles()
        {
            foreach (Hole hole in holes)
            {
                hole.IsThereMole = false;
                hole.IsThereBomb = false;
            }
        }

        private void CreateHoles()
        {
            holes.Add(new Hole(Hole1, 1));
            holes.Add(new Hole(Hole2, 2));
            holes.Add(new Hole(Hole3, 3));
            holes.Add(new Hole(Hole4, 4));
            holes.Add(new Hole(Hole5, 5));
            holes.Add(new Hole(Hole6, 6));
            holes.Add(new Hole(Hole7, 7));
        }
    }
}
