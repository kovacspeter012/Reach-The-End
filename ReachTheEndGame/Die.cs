using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    public static class Die
    {
        public static Style? _s;
        public static Ellipse[] DieButtons = new Ellipse[9];
        public static int DieNumber = 3;
        public static Rectangle DieRect;
        public static bool isDiceAllowed = true;
        
        private static Random rnd = new Random();
        public static void Throw()
        {
            DieNumber = rnd.Next(1,7);
        }
        public static void GenerateDieButtons(Canvas cnv)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Ellipse l = new() { Style = _s, IsHitTestVisible = false };
                    cnv.Children.Add(l);
                    Canvas.SetLeft(l, (cnv.ActualWidth - l.Width) / 2 + i*30);
                    Canvas.SetTop(l, (cnv.ActualHeight - l.Height) / 2 + j*30);
                    l.Visibility = Visibility.Hidden;

                    DieButtons[(i + 1) * 3 + (j + 1)] = l;
                }
            }
        }
        public static void CenterDie (Canvas cnv, Rectangle die)
        {
            Canvas.SetLeft(die, (cnv.ActualWidth - die.Width) / 2);
            Canvas.SetTop(die, (cnv.ActualHeight - die.Height) / 2);
        }

        public static void DisplayDie(Rectangle die, int num)
        {
            if (!isDiceAllowed) return; 
            for(int i = 0; i < 9; i++)
            {
                DieButtons[i].Visibility = Visibility.Hidden;
            }
            die.Fill = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(50,200), (byte)rnd.Next(50, 200), (byte)rnd.Next(50, 200)));
            switch (num)
            {
                case 1:
                    DieButtons[4].Visibility = Visibility.Visible;
                    return;
                case 2:
                    DieButtons[2].Visibility = Visibility.Visible;
                    DieButtons[6].Visibility = Visibility.Visible;
                    return;
                case 3:
                    DieButtons[2].Visibility = Visibility.Visible;
                    DieButtons[4].Visibility = Visibility.Visible;
                    DieButtons[6].Visibility = Visibility.Visible;
                    return;
                case 4:
                    DieButtons[0].Visibility = Visibility.Visible;
                    DieButtons[2].Visibility = Visibility.Visible;
                    DieButtons[6].Visibility = Visibility.Visible;
                    DieButtons[8].Visibility = Visibility.Visible;
                    return;
                case 5:
                    DieButtons[0].Visibility = Visibility.Visible;
                    DieButtons[2].Visibility = Visibility.Visible;
                    DieButtons[4].Visibility = Visibility.Visible;
                    DieButtons[6].Visibility = Visibility.Visible;
                    DieButtons[8].Visibility = Visibility.Visible;
                    return;
                case 6:
                    DieButtons[0].Visibility = Visibility.Visible;
                    DieButtons[1].Visibility = Visibility.Visible;
                    DieButtons[2].Visibility = Visibility.Visible;
                    DieButtons[7].Visibility = Visibility.Visible;
                    DieButtons[6].Visibility = Visibility.Visible;
                    DieButtons[8].Visibility = Visibility.Visible;
                    return;

            }
        }
    }
}
