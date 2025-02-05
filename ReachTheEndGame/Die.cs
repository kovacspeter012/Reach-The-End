using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    public static class Die
    {
        public static Style? _s;
        public static int Throw()
        {
            Random rnd = new Random();
            return rnd.Next(1,7);
        }
        public static void GenerateDieButtons(Canvas cnv)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Ellipse l = new() { Style = _s };
                    cnv.Children.Add(l);
                    Canvas.SetLeft(l, (cnv.ActualWidth - l.Width) / 2 + i*30);
                    Canvas.SetTop(l, (cnv.ActualHeight - l.Height) / 2 + j*30);
                    l.Visibility = Visibility.Hidden;
                }
            }
        }
        public static void CenterDie (Canvas cnv, Rectangle die)
        {
            Canvas.SetLeft(die, (cnv.ActualWidth - die.Width) / 2);
            Canvas.SetTop(die, (cnv.ActualHeight - die.Height) / 2);
        }
    }
}
