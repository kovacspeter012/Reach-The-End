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
    public static class Table
    {
        public static Style? _s;
        public static List<GameGrid> _grids;

        private readonly static Dictionary<GameGridType, int> NumberOfGrids = new Dictionary<GameGridType, int>
        {
            { GameGridType.Blank, 13 },
            { GameGridType.Backwards, 13 },
            { GameGridType.Double, 13 },
            { GameGridType.Choose, 13 },
            { GameGridType.GuessGame, 13 },
            { GameGridType.MemoryGame, 13 },
            { GameGridType.MoleGame, 13 },
            { GameGridType.MineGame, 13 }
        };
        public static string GetColor(GameGridType gameGridType)
        {
            switch (gameGridType)
            {
                case GameGridType.Blank: return "#ffffff";
                case GameGridType.Backwards: return "#ff0000";
                case GameGridType.Double: return "#47dc22";
                case GameGridType.Choose: return "#47dc22";
                case GameGridType.GuessGame: return "#47dc22";
                case GameGridType.MemoryGame: return "#f5f509";
                case GameGridType.MoleGame: return "#b1894d";
                case GameGridType.MineGame: return "#00a0fe";


                default: return "#000000";
            }
        }
        public static Rectangle GenerateRectangle(GameGridType gameGridType)
        {
            return new Rectangle() { Style = _s, Fill =  new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor(gameGridType)))};
        }
        public static void GenerateGrids(out List<GameGrid> grids)
        {
            grids = new List<GameGrid>();
            Dictionary<GameGridType, int> numOfGrids = new(NumberOfGrids);
            numOfGrids[GameGridType.Blank] -= 2;

            List<GameGridType> gameGrids = new();
            foreach (var kvp in numOfGrids)
            {
                for(int i = 0; i < kvp.Value; i++)
                {
                    gameGrids.Add(kvp.Key);
                }
            }
            var gameGridsArray = gameGrids.ToArray();

            grids.Add(new GameGrid(GenerateRectangle(GameGridType.Blank), GameGridType.Blank, isStart:true));

            Random r = new Random();
            int max = r.Next(3, 15);
            for (int i = 0; i < max; i++)
            {
                r.Shuffle(gameGridsArray);
            }
            
            foreach (GameGridType gameGridType in gameGridsArray)
            {
                grids.Add(new GameGrid(GenerateRectangle(gameGridType), gameGridType));
            }

            grids.Add(new GameGrid(GenerateRectangle(GameGridType.Blank), GameGridType.Blank, isEnd: true));

            return;
        }

        public static void GenerateTable(Canvas cnvGame)
        {
            GenerateGrids(out List<GameGrid> grids);
            _grids = grids;

            Random r = new();
            foreach (GameGrid grid in grids)
            {
                cnvGame.Children.Add(grid.Rectangle);
                Canvas.SetTop(grid.Rectangle, r.Next((int)cnvGame.ActualHeight));
                Canvas.SetLeft(grid.Rectangle, r.Next((int)cnvGame.ActualWidth));
            }
        }
    }
}
