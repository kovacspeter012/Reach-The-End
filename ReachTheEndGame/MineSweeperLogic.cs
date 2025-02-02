using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ReachTheEndGame
{
    class MineGameGrid
    {
        public Label Label { get; private set; }
        public int Index { get; private set; }
        public bool IsBomb { get; set; } = false;
        public bool IsRevealed { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public int BombsAround { get; set; } = 0;

        public MineGameGrid(Label label, int index)
        {
            Label = label;
            Index = index;
        }
    }

    static class MineGameLogic
    {
        public static string Flag = "🏴";
        public static string Bomb = "💣";
        public static void GenerateBombs(MineGameGrid[] gameGrids, List<MineGameGrid> bombGrids, int clickIndex, int bombCount)
        {
            var places = Enumerable.Range(0, 64).ToList();
            places.RemoveAt(clickIndex);

            Random r = new();
            for (int i = 0; i < bombCount; i++)
            {
                int random = r.Next(places.Count);
                int newBombIndex = places[random];

                gameGrids[newBombIndex].IsBomb = true;
                bombGrids.Add(gameGrids[newBombIndex]);

                places.RemoveAt(random);
            }
        }
        public static void DoAroundGrids(Action<int,int> callback, MineGameGrid gameGrid)
        {
            int row = gameGrid.Index / 8;
            int col = gameGrid.Index % 8;

            foreach (int i in new[] { -1, 0, 1 })
            {
                foreach (int j in new[] { -1, 0, 1 })
                {
                    if (i == 0 && j == 0) continue;
                    if (7 < row + i || row + i < 0) continue;
                    if (7 < col + j || col + j < 0) continue;

                    callback(row+i,col+j);

                }
            }
        }
        public static void SetBombsAround(MineGameGrid[] gameGrids, List<MineGameGrid> bombGrids)
        {
            foreach (var bombGrid in bombGrids)
            {
                DoAroundGrids((row,col) => gameGrids[(row) * 8 + (col)].BombsAround += 1,bombGrid);
            }
        }
        public static void ShowZeroesAround(MineGameGrid[] gameGrids, MineGameGrid zeroGrid)
        {
            DoAroundGrids((row, col) =>
            {
                if (gameGrids[row * 8 + col].IsRevealed) return;
                gameGrids[row * 8 + col].IsRevealed = true;

                if (gameGrids[row * 8 + col].BombsAround == 0)
                {
                    ShowZeroesAround(gameGrids, gameGrids[row * 8 + col]);
                }
            }, zeroGrid);
        }
        public static void ShowCheat(MineGameGrid[] MineGameGrids)
        {
            foreach (MineGameGrid gameGrid in MineGameGrids)
            {
                ShowValue(gameGrid);
            }
        }
        public static void ShowValue(MineGameGrid gameGrid)
        {
            if (!gameGrid.IsRevealed)
            {
                if (gameGrid.IsFlagged)
                {
                    gameGrid.Label.Content = "🏴";
                    gameGrid.Label.Foreground = Brushes.Red;
                }
                else gameGrid.Label.Content = "";

                return;
            }
            //revealed
            if (gameGrid.IsBomb)
            {
                gameGrid.Label.Content = "💣";
                gameGrid.Label.Foreground = Brushes.Black;
            }
            else
            {
                gameGrid.Label.Background = Brushes.Gray;

                if (gameGrid.BombsAround != 0)
                {
                    gameGrid.Label.Content = gameGrid.BombsAround;
                    gameGrid.Label.Foreground = GetBrushForNumber(gameGrid.BombsAround);
                }
                else
                {
                    gameGrid.Label.Content = "";
                }
            }
        }
        public static SolidColorBrush GetBrushForNumber(int num)
        {
            switch (num)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.DarkRed;
                case 6: return Brushes.Cyan;
                case 7: return Brushes.Black;
                case 8: return Brushes.LightGray;
                default: return Brushes.Gold;
            }
        }
        public static int GetActiveBombsCount(List<MineGameGrid> Bombs)
        {
            return Bombs.Count(e => !e.IsFlagged);
        }
    }
}
