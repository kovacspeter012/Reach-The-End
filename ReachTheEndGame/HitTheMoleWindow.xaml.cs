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
        int foundMolesNum = 0;
        public HitTheMoleWindow()
        {
            InitializeComponent();
            DispatcherTimer aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += (sender, e) =>
            {
                resetHoles();
                lblFoundMoles.Content = $"Ennyi vakondot kaptál el: {foundMolesNum.ToString()}";
                timeLeft -= 1;
                lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
                int randomHoleIndex1 = random.Next(0, 7);
                int randomHoleIndex2 = random.Next(0, 7);
                while (randomHoleIndex1 == randomHoleIndex2)
                {
                    randomHoleIndex1 = random.Next(0, 7);
                    randomHoleIndex2 = random.Next(0, 7);
                }
                SetMoleOrBomb(randomHoleIndex1);
                SetMoleOrBomb(randomHoleIndex2);


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
                            hole.IsThereMole = false;
                            foundMolesNum++;
                        }
                        else if (hole.IsThereBomb)
                        {
                            
                        }
                    };
                }
            };
        }

        private void SetMoleOrBomb(int randomHoleIndex)
        {
            int randomImage = random.Next(0, 3);
            if (randomImage == 0)
            {
                holes[randomHoleIndex].IsThereBomb = true;
            }
            else
            {
                holes[randomHoleIndex].IsThereMole = true;
            }
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
