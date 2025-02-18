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
    public partial class HitTheMoleWindow : Window, IMiniGame
    {
        private int timeLeft = 30;
        private List<Hole> holes = new List<Hole>();
        Random random = new Random();
        int foundMolesNum = 0;
        public GameEndHandler GameEndHandler { get; set; }
        DispatcherTimer aTimer = new DispatcherTimer();
        public HitTheMoleWindow()
        {
            InitializeComponent();
            
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += (sender, e) =>
            {
                resetHoles();
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
                    if (foundMolesNum == 0)
                    {
                        aTimer.Stop();
                        EndGame(false, false, 1, 1, $"Lejárt az időd. Nem kaptál el egy vakondot sem. Vissza fogsz egyet lépni.");
                    }
                    else 
                    {
                        aTimer.Stop();
                        EndGame(true, true, 0, 0.5, $"Lejárt az időd. Csak {foundMolesNum.ToString()} vakondot kaptál el. Kockadobásod felével léphetsz tovább.");
                    }
                    
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
                            lblFoundMoles.Content = $"Ennyi vakondot kaptál el: {foundMolesNum.ToString()}";
                            if (foundMolesNum >= 10)
                            {
                                aTimer.Stop();
                                EndGame(true, true, 0, 2, "Időben elfogtál 10 vakondot! Kockadobásod kétszeresével léphetsz tovább!");
                            }
                        }
                        else if (hole.IsThereBomb)
                        {
                            aTimer.Stop();
                            EndGame(false, false, 6, 1, "Felrobbantál! Büntetésül hatot fogsz visszalépni.");
                        }
                    };
                }
            };
        }

        private void EndGame(bool win, bool requireDiceAfter, int extraSteps, double diceMultiplyer, string message)
        {
            GameEndHandler = new(win, requireDiceAfter, extraSteps, diceMultiplyer, false, message);
            window.Close();
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
