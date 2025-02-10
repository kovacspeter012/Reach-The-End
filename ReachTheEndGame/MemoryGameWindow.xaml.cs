using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using System.IO;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for MemoryGameWindow.xaml
    /// </summary>
    public partial class MemoryGameWindow : Window
    {
        private int timeLeft = 30;
        public MemoryGameWindow()
        {
            InitializeComponent();
            DispatcherTimer aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += OnTimedEvent;
            aTimer.Start();

            Loaded += (sender, e) =>
            {
                test.Content = Directory.GetCurrentDirectory();
                int i = 1;
                Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame", "*png").ToList().ForEach(file =>
                {
                    var brd = new Border()
                    {
                        Name = $"bCard{i}",
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(3),
                        Margin = new Thickness(5),
                    };
                    var img = new Image()
                    {
                        Name = $"iCard{i}",
                        Width = 80,
                        Source = new BitmapImage(new Uri(file))
                    };
                    brd.Child = img;
                    if (i < 7)
                    {
                        wpFirstSix.Children.Add(brd);
                    }
                    else if (i < 13)
                    {
                        wpSecondSix.Children.Add(brd);
                    }
                    else
                    {
                        wpThirdSix.Children.Add(brd);
                    }
                    i++;
                });
            };

        }

        public void OnTimedEvent(object source, EventArgs e)
        {
            timeLeft -= 1;
            lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
        }
    }
}
