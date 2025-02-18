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
using System.Reflection;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for MemoryGameWindow.xaml
    /// </summary>
    public partial class MemoryGameWindow : Window, IMiniGame
    {
        Random rand = new Random();

        private int timeLeft = 30;

        List<Card> cards = new List<Card>();

        Dictionary<int, Image> ImageRef = new Dictionary<int, Image>();

        int foundPairs = 0;

        public GameEndHandler GameEndHandler { get; set; } = new GameEndHandler(false, false, 6, 1, false, "Kiléptél a játékból, ezért hat mezővel hátrébb fogsz menni.");

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
                    if (foundPairs > 0)
                    {
                        EndGame(true, false, foundPairs, 1);
                    }
                    else
                    {
                        EndGame(false, true, 0, 1); // csak kockadobással menjen hátra
                    }
                }
            };
            aTimer.Start();

            Loaded += (sender, e) =>
            {
                MakeCards();
                ShowCards();
            };
        }

        private void EndGame(bool win, bool requireDiceAfter, int extraSteps, double diceMultiplyer)
        {
            GameEndHandler = new(win, requireDiceAfter, extraSteps, diceMultiplyer, false, win ? $"Megtaláltál {extraSteps} párt! Ennyivel léphetsz tovább." : "Sajnos egy párt sem találtál meg! A dobásod értékével fogsz visszafelé lépni.");
            window.Close();
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
                    if (item.IsFound == false)
                    {
                        img.Source = new BitmapImage(new Uri(item.FrontImage));
                        item.IsFlipped = true;
                        CheckForPair();
                    }  
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
            window.PreviewMouseLeftButtonDown += NoClick;
            List<Card> ToBeCheckedCardList = new List<Card>();
            foreach (var item in cards)
            {
                if (item.IsFlipped == true && item.IsFound == false)
                {
                    ToBeCheckedCardList.Add(item);
                }
                
            }
            if (ToBeCheckedCardList.Count > 1)
            {
                if (ToBeCheckedCardList[0].FrontImage == ToBeCheckedCardList[1].FrontImage)
                {
                    foundPairs += 1;
                    lblFoundPairsCount.Content = foundPairs;
                    
                    ToBeCheckedCardList[0].IsFound = true;
                    ToBeCheckedCardList[1].IsFound = true;
                }

                await Task.Delay(1000);
                foreach (var item in cards)
                {
                    if (item.IsFlipped == true && item.IsFound == false)
                    {
                        item.IsFlipped = false;
                        ImageRef[item.Id].Source = new BitmapImage(new Uri(item.BackImage));   
                    }
                }
            }
            window.PreviewMouseLeftButtonDown -= NoClick;
            if (foundPairs == 6)
            {
                EndGame(true, false, foundPairs, 0);
            }
        }

        private void NoClick(object sender, RoutedEventArgs e)
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
                Cards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i, false, false));
                i++;
                if (ImageList.Count > 6)
                {
                    Cards.Add(new Card($"{Directory.GetCurrentDirectory()}\\Images\\MemoryGame\\CardBack.png", ImageList[selectIndex], i, false, false));
                    i++;
                }
                ImageList.RemoveAt(selectIndex);

            }

            cards = Cards.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
