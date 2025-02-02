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

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for MineSweeper.xaml
    /// </summary>
    public partial class MineSweeper : Window
    {
        List<MineGameGrid> Bombs { get; set; }
        List<MineGameGrid> Flags { get; set; }
        List<MineGameGrid> FlaggedBombs => Bombs.Where(e=>e.IsFlagged).ToList();
        List<MineGameGrid> ActiveBombs => Bombs.Where(e=>!e.IsFlagged).ToList();

        MineGameGrid[] MineGameGrids;
        bool IsGameStarted { get; set; } = false;

        public bool IsGameWon { get; private set; }
        public int ActiveBombsCount => Bombs.Where(e=>!e.IsFlagged).Count();

        public MineSweeper()
        {
            Bombs = new();
            Flags = new();
            MineGameGrids = new MineGameGrid[64];

            InitializeComponent();
            Loaded += MineSweeper_Loaded;
        }

        private void MineSweeper_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                grdGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1,GridUnitType.Star)});
                grdGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Label l = new Label() { Content = "", Style= this.FindResource("GameGridStyle") as Style};
                    grdGame.Children.Add(l);
                    Grid.SetRow(l, i);
                    Grid.SetColumn(l, j);

                    if(i == 0)
                    {
                        l.Margin = new Thickness(2,4,2,2);
                    }

                    MineGameGrids[i*8+j] = new MineGameGrid(l,i*8+j);

                    l.PreviewMouseDown += lblClick;
                }
            }
            lblBombs.Content = $"{MineGameLogic.Bomb}\n10";
            lblFlags.Content = $"{MineGameLogic.Flag}\n10";
        }

        private void lblClick(object sender, MouseButtonEventArgs e)
        {
            MouseButton b = e.ChangedButton;

            if (sender is not Label) return;
            Label label = (Label)sender;

            MineGameGrid? mineGameGrid = MineGameGrids.FirstOrDefault(e=>e.Label == label);
            if (mineGameGrid is null) return;

            //jobb egérgomb
            if (b == MouseButton.Right)
            {
                if(IsGameStarted && !mineGameGrid.IsRevealed)
                {
                    if(mineGameGrid.IsFlagged)
                    {
                        mineGameGrid.IsFlagged = !mineGameGrid.IsFlagged;
                        Flags.Remove(mineGameGrid);
                    }
                    else if(Flags.Count < Bombs.Count)
                    {
                        mineGameGrid.IsFlagged = !mineGameGrid.IsFlagged;
                        Flags.Add(mineGameGrid);
                    }
                    lblFlags.Content = $"{MineGameLogic.Flag}\n{new string(' ', Bombs.Count - Flags.Count < 10 ? 1 : 0 )}{Bombs.Count - Flags.Count}";
                    MineGameLogic.ShowValue(mineGameGrid);

                    if (MineGameGrids.All(e => (e.IsBomb && e.IsFlagged && !e.IsRevealed) || (!e.IsBomb && !e.IsFlagged && e.IsRevealed)))
                    {
                        IsGameWon = true;
                        this.Close();
                    }
                }
                return;
            }

            //bal egérgomb

            if (!IsGameStarted)
            {
                MineGameLogic.GenerateBombs(MineGameGrids, Bombs, mineGameGrid.Index, 10);
                MineGameLogic.SetBombsAround(MineGameGrids, Bombs);

                IsGameStarted = true;
            }
            if (mineGameGrid.IsFlagged) return;

            mineGameGrid.IsRevealed = true;
            if (mineGameGrid.IsBomb)
            {
                int explodedBombs = MineGameLogic.GetActiveBombsCount(Bombs);
                lblBombs.Content = $"Bumm: {explodedBombs} bombs";
            }
            if (mineGameGrid.BombsAround == 0)
            {
                MineGameLogic.ShowZeroesAround(MineGameGrids,mineGameGrid);
            }

            foreach (var l in MineGameGrids)
            {
                if (l.IsRevealed)
                {
                    MineGameLogic.ShowValue(l);
                    if (l.IsFlagged)
                    {
                        l.IsFlagged = false;
                        Flags.Remove(l);
                    }
                }
            }
            lblFlags.Content = $"{MineGameLogic.Flag}\n{new string(' ', Bombs.Count - Flags.Count < 10 ? 1 : 0)}{Bombs.Count - Flags.Count}";

            if (MineGameGrids.Where(e => e.IsBomb && e.IsRevealed).Any())
            {
                IsGameWon = false;
                this.Close();
            }
            if (MineGameGrids.All(e => (e.IsBomb && e.IsFlagged && !e.IsRevealed) || (!e.IsBomb && !e.IsFlagged && e.IsRevealed)))
            {
                IsGameWon = true;
                this.Close();
            }

        }

    }
}
