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
        }

        public void OnTimedEvent(object source, EventArgs e)
        {
            timeLeft -= 1;
            lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
        }
    }
}
