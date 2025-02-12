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

        Dictionary<int, Image> ImageRef = new Dictionary<int, Image>();
        public MemoryGameWindow()
        {
            InitializeComponent();
            DispatcherTimer aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.Tick += (sender, e) =>
            {
                timeLeft -= 1;
                lblTimer.Content = $"Ennyi mp van vissza: {timeLeft}";
                if (timeLeft < 1)
                {
                    aTimer.Stop();
                }
            };
            aTimer.Start();

            Loaded += (sender, e) =>
            {
                MakeCards();
                ShowCards();
            };
            test.Content = lblFoundPairsCount.Content.ToString();
        }

        
        private void ShowCards()
        {
            int i = 1;
            foreach (var item in cards)
            {
                var brd = new Border()
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(3),
                    Margin = new Thickness(5),
                };
                var img = new Image()
                {
                    Name = $"iCard{item.Id}",
                    Width = 80,
                    Height = 80,
                    Source = new BitmapImage(new Uri(item.BackImage)),
                };
                ImageRef.Add(item.Id, img);
                img.MouseLeftButtonDown += (sender, e) =>
                {
                    img.Source = new BitmapImage(new Uri(item.FrontImage));
                    item.IsFlipped = true;
                    CheckForPair();
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

        async private void CheckForPair()
        {
            List<Card> ToBeCheckedCardList = new List<Card>();
            foreach (var item in cards)
            {
                if (item.IsFlipped == true)
                {
                    ToBeCheckedCardList.Add(item);
                }
                ImageRef[item.Id].MouseLeftButtonDown -= (sender, e) =>
                {
                    e.Handled = true;
                };
            }
            if (ToBeCheckedCardList.Count > 1)
            {
                if (ToBeCheckedCardList[0].FrontImage == ToBeCheckedCardList[1].FrontImage)
                {
                    lblFoundPairsCount.Content = int.Parse(lblFoundPairsCount.Content.ToString()) + 1;
                }
                
                await Task.Delay(2000);
                foreach (var item in cards)
                {
                    if (item.IsFlipped == true)
                    {
                        item.IsFlipped = false;
                        ImageRef[item.Id].Source = new BitmapImage(new Uri(item.BackImage));   
                    }
                    ImageRef[item.Id].MouseLeftButtonDown += (sender, e) =>
                    {
                        ImageRef[item.Id].Source = new BitmapImage(new Uri(item.FrontImage));
                        item.IsFlipped = true;
                        CheckForPair();
                    };
                }
            }
        }

        private void PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void MakeCards()
        {
            List<string> notShuffledImageList = new List<string>();
            List<string> ImageList = new List<string>();
            Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame", "*png").ToList().ForEach(file =>
            {
                if (file != $"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png")
                {
                    notShuffledImageList.Add(file);
                }
            });

            ImageList = notShuffledImageList.OrderBy(x => Guid.NewGuid()).ToList();

            List<Card> Cards = new List<Card>();
            int i = 0;
            while (ImageList.Count > 0)
            {

                int selectIndex = rand.Next(0, ImageList.Count);
                Cards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i, false));
                i++;
                if (ImageList.Count > 6)
                {
                    Cards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i, false));
                    i++;
                }
                ImageList.RemoveAt(selectIndex);

            }

            cards = Cards.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
