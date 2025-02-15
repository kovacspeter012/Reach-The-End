using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    public static class Table
    {
        public static Style? _s;
        public static List<GameGrid> _grids;
        public static List<Section> _sections;

        private readonly static Dictionary<GameGridType, int> NumberOfGrids = new Dictionary<GameGridType, int> //118
        {
            { GameGridType.Blank, 27 },
            { GameGridType.Backwards, 13 },
            { GameGridType.Double, 13 },
            { GameGridType.Choose, 13 },
            { GameGridType.GuessGame, 13 },
            { GameGridType.MemoryGame, 13 },
            { GameGridType.MoleGame, 13 },
            { GameGridType.MineGame, 13 }
        };
        private static string GetColor(GameGridType gameGridType)
        {
            switch (gameGridType)
            {
                case GameGridType.Blank: return "#ffffff";
                case GameGridType.Backwards: return "#ff0000";
                case GameGridType.Double: return "#47dc22";
                case GameGridType.Choose: return "#b300fe";
                case GameGridType.GuessGame: return "#fea200";
                case GameGridType.MemoryGame: return "#f5f509";
                case GameGridType.MoleGame: return "#b1894d";
                case GameGridType.MineGame: return "#00a0fe";


                default: return "#000000";
            }
        }
        private static Rectangle GenerateRectangle(GameGridType gameGridType)
        {
            return new Rectangle() { Style = _s, Fill =  new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor(gameGridType)))};
        }
        private static void GenerateGrids(out List<GameGrid> grids)
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
        private static void GenerateSections(out List<Section> sections)
        {
            sections = [];

            for (int i = 0;i < _grids.Count;i++)
            {
                if (i == 0) sections.Add(new Section(new GamePattern(0, -35)));
                if (i == 15) sections.Add(new Section(new GamePattern(35,0)));
                if (i == 17) sections.Add(new Section(new GamePattern(0,-35)));
                if (i == 18) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 28) sections.Add(new Section(new GamePattern(0, 35)));
                if (i == 29) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 39) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 40) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 41) sections.Add(new Section(new GamePattern(0, 35)));
                if (i == 42) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 45) sections.Add(new Section(new GamePattern(0, 35))); // 10
                if (i == 48) sections.Add(new Section(new GamePattern(-35, 0)));
                if (i == 56) sections.Add(new Section(new GamePattern(0, 35)));
                if (i == 58) sections.Add(new Section(new GamePattern(-35, 0)));
                if (i == 64) sections.Add(new Section(new GamePattern(0, 35)));
                if (i == 72) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 77) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 83) sections.Add(new Section(new GamePattern(0, 35)));
                if (i == 91) sections.Add(new Section(new GamePattern(-35, 0)));
                if (i == 96) sections.Add(new Section(new GamePattern(-35, 0)));
                if (i == 97) sections.Add(new Section(new GamePattern(0, -35))); //20
                if (i == 100) sections.Add(new Section(new GamePattern(-35, 0)));
                if (i == 104) sections.Add(new Section(new GamePattern(0, -35)));
                if (i == 107) sections.Add(new Section(new GamePattern(35, 0)));
                if (i == 114) sections.Add(new Section(new GamePattern(0, 35)));

                _grids[i].Section = sections.Last();
                _grids[i].Section.Elements.Add(_grids[i]);
            }
            sections[0].Starts.Add(sections[0].Elements.First());
            sections[0].Ends.Add(sections[1].Elements.First());

            sections[1].Starts.Add(sections[0].Elements.Last());
            sections[1].Ends.Add(sections[2].Elements.First());
            sections[1].Ends.Add(sections[4].Elements.First());

            sections[2].Starts.Add(sections[1].Elements.Last());
            sections[2].Ends.Add(sections[3].Elements.First());

            sections[3].Starts.Add(sections[2].Elements.Last());
            sections[3].Ends.Add(sections[6].Elements.First()); //

            sections[4].Starts.Add(sections[1].Elements.Last());
            sections[4].Ends.Add(sections[5].Elements.First());

            sections[5].Starts.Add(sections[4].Elements.Last());
            sections[5].Ends.Add(sections[7].Elements.First()); //

            sections[6].Starts.Add(sections[3].Elements.Last());
            sections[7].Starts.Add(sections[5].Elements.Last());

            sections[6].Ends.Add(sections[8].Elements.First());
            sections[7].Ends.Add(sections[8].Elements.First());

            sections[8].Starts.Add(sections[6].Elements.Last());
            sections[8].Starts.Add(sections[7].Elements.Last());

            sections[8].Ends.Add(sections[9].Elements.First());
            sections[9].Starts.Add(sections[8].Elements.Last());

            sections[9].Ends.Add(sections[10].Elements.First());
            sections[10].Starts.Add(sections[9].Elements.Last());

            sections[10].Ends.Add(sections[11].Elements.First());
            sections[11].Starts.Add(sections[10].Elements.Last());

            sections[11].Ends.Add(sections[12].Elements.First());
            sections[12].Starts.Add(sections[11].Elements.Last());

            sections[12].Ends.Add(sections[13].Elements.First());
            sections[12].Ends.Add(sections[16].Elements.First());
            sections[13].Starts.Add(sections[12].Elements.Last());

            sections[13].Ends.Add(sections[14].Elements.First());
            sections[14].Starts.Add(sections[13].Elements.Last());

            sections[14].Ends.Add(sections[15].Elements.First());
            sections[15].Starts.Add(sections[14].Elements.Last());


            sections[16].Starts.Add(sections[12].Elements.Last());

            sections[16].Ends.Add(sections[17].Elements.First());
            sections[17].Starts.Add(sections[16].Elements.Last());

            sections[17].Ends.Add(sections[18].Elements.First());
            sections[18].Starts.Add(sections[17].Elements.Last());

            sections[15].Ends.Add(sections[19].Elements.First());
            sections[18].Ends.Add(sections[19].Elements.First());
            sections[19].Starts.Add(sections[18].Elements.Last());
            sections[19].Starts.Add(sections[15].Elements.Last());

            sections[19].Ends.Add(sections[20].Elements.First());
            sections[20].Starts.Add(sections[19].Elements.Last());
            
            sections[20].Ends.Add(sections[21].Elements.First());
            sections[21].Starts.Add(sections[20].Elements.Last());

            sections[21].Ends.Add(sections[22].Elements.First());
            sections[22].Starts.Add(sections[21].Elements.Last());

            sections[22].Ends.Add(sections[23].Elements.First());
            sections[23].Starts.Add(sections[22].Elements.Last());

            sections[23].Ends.Add(sections[24].Elements.First());
            sections[24].Starts.Add(sections[23].Elements.Last());

            sections[24].Ends.Add(sections[24].Elements.Last());

        }
        private static void TestWays(GameGrid selectedGrid)
        {
            _grids.ForEach(el => el.Rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor((GameGridType)(23)))));
            selectedGrid.Section.Elements.ForEach(el => el.Rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor((GameGridType)(1)))));
            selectedGrid.Section.Starts.ForEach(el => el.Rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor((GameGridType)(2)))));
            selectedGrid.Section.Ends.ForEach(el => el.Rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor((GameGridType)(3)))));
        }
        private static void PlaceGridsOnCanvas(Canvas cnvGame, bool testPatterns)
        {
            GameGrid prevElement = _sections[0].Starts[0];
            var ElementCords = (13.0, cnvGame.ActualHeight - 7.0);

            int currentPatternId = 0;
            bool isFirstSection = true;
            foreach (Section s in _sections)
            {
                prevElement = s.Starts[0];

                if (isFirstSection) isFirstSection = false;
                else ElementCords = ((int)Canvas.GetLeft(prevElement.Rectangle), (int)Canvas.GetTop(prevElement.Rectangle));

                foreach (GameGrid grid in s.Elements)
                {
                    ElementCords = (ElementCords.Item1 + grid.Section.gamePattern.OffSetX, ElementCords.Item2 + grid.Section.gamePattern.OffSetY);
                    Canvas.SetLeft(grid.Rectangle, ElementCords.Item1);
                    Canvas.SetTop(grid.Rectangle, ElementCords.Item2);
                    prevElement = grid;


                    if (testPatterns) grid.Rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor((GameGridType)(currentPatternId % 8))));
                }
                currentPatternId++;
            }
        }
        private static void PlaceGridsInCanvas(Canvas cnvGame, bool testConnections)
        {
            foreach (GameGrid grid in _grids)
            {
                cnvGame.Children.Add(grid.Rectangle);
                if (testConnections) grid.Rectangle.PreviewMouseDown += (s, e) => TestWays(grid);
            }
        }
        [TestingAttribute(
            ShowConnections = false,
            ShowPatterns = false
        )]
        public static void GenerateTable(Canvas cnvGame)
        {
            var testAttr = typeof(Table).GetMethod("GenerateTable")?.GetCustomAttribute<TestingAttribute>();
            bool testConnections = testAttr is not null && testAttr.ShowConnections;
            bool testPatterns = testAttr is not null && testAttr.ShowPatterns;

            GenerateGrids(out List<GameGrid> grids);
            _grids = grids;

            GenerateSections(out List<Section> sections);
            _sections = sections;

            PlaceGridsInCanvas(cnvGame, testConnections);

            PlaceGridsOnCanvas(cnvGame, testPatterns);
        }
    }
}
