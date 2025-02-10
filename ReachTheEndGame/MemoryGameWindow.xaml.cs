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
        Random rand = new Random();

        private int timeLeft = 30;

        List<Card> cards = new List<Card>();
        public MemoryGameWindow()
        {
            InitializeComponent();
            DispatcherTimer aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += OnTimedEvent;
            aTimer.Start();

            Loaded += (sender, e) =>
            {
                MakeCards();
                ShowCards();
            };

        }

        private void ShowCards()
        {
            int i = 1;
            foreach (var item in cards)
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
                    Height = 80,
                    Source = new BitmapImage(new Uri(item.BackImage))
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
            }
        }

        private void MakeCards()
        {
            List<string> ImageList = new List<string>();
            Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame", "*png").ToList().ForEach(file =>
            {
                if (file != $"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png")
                {
                    ImageList.Add(file);
                }
            });

            List<Card> notShuffledCards = new List<Card>();
            int i = 0;
            while (ImageList.Count > 0)
            {

                int selectIndex = rand.Next(0, ImageList.Count);
                notShuffledCards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i));
                i++;
                if (ImageList.Count > 6)
                {
                    notShuffledCards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i-1));
                    i++;
                }
                ImageList.RemoveAt(selectIndex);

            }
            cards = notShuffledCards.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public void OnTimedEvent(object source, EventArgs e)
        {
            timeLeft -= 1;
            lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
        }

        
    }
}
