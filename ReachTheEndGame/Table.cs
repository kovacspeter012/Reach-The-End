using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReachTheEndGame
{
    public static class Table
    {
        public static Style? _s;
        public static List<GameGrid> _grids;
        public static List<Section> _sections;

        public static MainWindow MainWindow { get; set; }

        public static int SectionID = 0;
        public static int SectionElementID = 0;
        public static GameGrid SelectedGameGrid => _sections[SectionID].Elements[SectionElementID];
        public static GameGrid PrevGameGrid;

        public static System.Timers.Timer Timer = new(300);
        public static System.Timers.Timer SubTimer = new(600);
        public static bool ShowSelectedGameGrid = true;
        public static bool DoBlinking = true;
        public static bool ShowSelectableGameGrids = true;
        public static List<GameGrid> subGameGrids = new(); 

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
        public static SolidColorBrush GetBrush(GameGridType gameGridType)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetColor(gameGridType)));
        }
        private static Rectangle GenerateRectangle(GameGridType gameGridType)
        {
            return new Rectangle() { Style = _s, Fill = GetBrush(gameGridType)};
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

            int id = 0;
            for (int i = 0;i < _grids.Count;i++)
            {
                if (i == 0) sections.Add(new Section(new GamePattern(0, -35),id++));
                if (i == 15) sections.Add(new Section(new GamePattern(35,0), id++));
                if (i == 17) sections.Add(new Section(new GamePattern(0,-35), id++));
                if (i == 18) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 28) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 29) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 39) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 40) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 41) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 42) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 45) sections.Add(new Section(new GamePattern(0, 35), id++)); // 10
                if (i == 48) sections.Add(new Section(new GamePattern(-35, 0), id++));
                if (i == 56) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 58) sections.Add(new Section(new GamePattern(-35, 0), id++));
                if (i == 64) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 72) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 77) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 83) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 91) sections.Add(new Section(new GamePattern(-35, 0), id++));
                if (i == 96) sections.Add(new Section(new GamePattern(-35, 0), id++));
                if (i == 97) sections.Add(new Section(new GamePattern(0, -35), id++)); //20
                if (i == 100) sections.Add(new Section(new GamePattern(-35, 0), id++));
                if (i == 104) sections.Add(new Section(new GamePattern(0, -35), id++));
                if (i == 107) sections.Add(new Section(new GamePattern(35, 0), id++));
                if (i == 114) sections.Add(new Section(new GamePattern(0, 35), id++));
                if (i == 117) sections.Add(new Section(new GamePattern(0, 35), id++));

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

            sections[24].Ends.Add(sections[25].Elements.Last());
            sections[25].Starts.Add(sections[24].Elements.Last());

            sections[25].Ends.Add(sections[25].Elements.Last());
        }
        private static void TestWays(GameGrid selectedGrid)
        {
            _grids.ForEach(el => el.Rectangle.Fill = GetBrush((GameGridType)(23)));
            selectedGrid.Section.Elements.ForEach(el => el.Rectangle.Fill = GetBrush((GameGridType)(1)));
            selectedGrid.Section.Starts.ForEach(el => el.Rectangle.Fill = GetBrush((GameGridType)(2)));
            selectedGrid.Section.Ends.ForEach(el => el.Rectangle.Fill = GetBrush((GameGridType)(3)));
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


                    if (testPatterns) grid.Rectangle.Fill = GetBrush((GameGridType)(currentPatternId % 8));
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
            PrevGameGrid = _sections[0].Starts[0];

            PlaceGridsInCanvas(cnvGame, testConnections);

            PlaceGridsOnCanvas(cnvGame, testPatterns);

            ChangeCharacterPlace();
        }
        public static void ChangeCharacterPlace()
        {
            PrevGameGrid.Rectangle.Fill = GetBrush(PrevGameGrid.GridType);
            SelectedGameGrid.Rectangle.Fill = GetBrush((GameGridType)(24));
        }
        public static IMiniGame GetGame(GameGridType gameGridType) => gameGridType switch
        {
            GameGridType.Blank => new GridBlank(),
            GameGridType.Backwards => new GridBackwards(),
            GameGridType.Double => new GridDouble(),
            GameGridType.Choose => new GridChoose(),

            GameGridType.GuessGame => new GridBlank(),
            GameGridType.MemoryGame => new GridBlank(),
            GameGridType.MoleGame => new GridBlank(),
            GameGridType.MineGame => new GridBlank(),

            _ => new GridBlank(),
        };
        private static Task<GameGrid> WaitForDirectionClick(List<GameGrid> DirectionGameGrids)
        {
            TaskCompletionSource<GameGrid> clickTask = new TaskCompletionSource<GameGrid>();

            void ClickHandler(object sender, EventArgs e)
            {
                subGameGrids = new();
                DoBlinking = true;
                SubTimer.Elapsed -= SubTimer_Tick;
                foreach (GameGrid grid in DirectionGameGrids)
                {
                    grid.Rectangle.PreviewMouseDown -= ClickHandler;
                    grid.Rectangle.Fill = GetBrush(grid.GridType);
                }
                GameGrid g = DirectionGameGrids.FirstOrDefault(gamegrid => gamegrid.Rectangle == (Rectangle)sender) ?? _sections[0].Elements[0];
                clickTask.SetResult(g);
            }

            foreach (GameGrid grid in DirectionGameGrids)
            {
                grid.Rectangle.PreviewMouseDown += ClickHandler;
                //grid.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(110,110,110));
                subGameGrids.Add(grid);
            }
            
            DoBlinking = false;
            SubTimer.Elapsed += SubTimer_Tick;
            return clickTask.Task;
        }
        private static Task<bool> WaitForDiceClick(Rectangle dice)
        {
            TaskCompletionSource<bool> clickTask = new TaskCompletionSource<bool>();

            void ClickHandler(object sender, EventArgs e)
            {
                dice.PreviewMouseDown -= ClickHandler;
                clickTask.SetResult(true);
            }

            dice.PreviewMouseDown += ClickHandler;
            return clickTask.Task;
        }
        private static async Task TakeSteps(GameEndHandler steps)
        {
            PrevGameGrid = SelectedGameGrid;

            var sectionsBefore = SelectedGameGrid.Section.Starts;
            var sectionsAfter = SelectedGameGrid.Section.Ends;

            GameGrid selectedSection = _sections[0].Starts[0];
            bool twoWayAlreadySelectedIt = false;
            if (steps.TwoWays)
            {
                List<GameGrid> Before = new();
                List<GameGrid> After = new();

                List<GameGrid> All = new();

                if(SectionElementID - 1 >= 0)
                {
                    Before.Add(_sections[SectionID].Elements[SectionElementID - 1]);
                }
                else
                {
                    Before.AddRange(sectionsBefore);
                }
                if(SectionElementID + 1 < _sections[SectionID].Elements.Count)
                {
                    After.Add(_sections[SectionID].Elements[SectionElementID + 1]);
                }
                else
                {
                    After.AddRange(sectionsAfter);
                }

                All.AddRange(Before);
                All.AddRange(After);

                var clicked = await WaitForDirectionClick(All);

                steps.Win = After.Contains(clicked);
                selectedSection = clicked;
                twoWayAlreadySelectedIt = true;
            }

            if (steps.RequireDiceAfter)
            {
                Die.isDiceAllowed = true;
                await WaitForDiceClick(Die.DieRect);
                Die.isDiceAllowed = false;
            }

            int MoveInt = steps.Win ? 1 : -1;
            int stepsToTake = (int)(Die.DieNumber * steps.DiceMultiplyer + steps.ExtraSteps);

            for (int i = 0; i < stepsToTake; i++)
            {
                PrevGameGrid = SelectedGameGrid;
                if (SectionElementID + MoveInt < 0)
                {
                    if (sectionsBefore.Count == 1) selectedSection = sectionsBefore[0];
                    else if(twoWayAlreadySelectedIt)
                    {
                        //twoway kiválasztotta
                    }
                    else
                    {
                        selectedSection = await WaitForDirectionClick(sectionsBefore);
                    }
                    SectionID = selectedSection.Section.ID;
                    SectionElementID = selectedSection.Section.Elements.Count - 1;

                    sectionsBefore = SelectedGameGrid.Section.Starts;
                    sectionsAfter = SelectedGameGrid.Section.Ends;
                }
                else if (SectionElementID + MoveInt >= _sections[SectionID].Elements.Count)
                {
                    if (sectionsAfter.Count == 1) selectedSection = sectionsAfter[0];
                    else if (twoWayAlreadySelectedIt)
                    {
                        //twoway kiválasztotta
                    }
                    else
                    {
                        selectedSection = await WaitForDirectionClick(sectionsAfter);
                    }
                    SectionID = selectedSection.Section.ID;
                    SectionElementID = 0;

                    sectionsBefore = SelectedGameGrid.Section.Starts;
                    sectionsAfter = SelectedGameGrid.Section.Ends;
                }
                else
                {
                    SectionElementID += MoveInt;
                }
                await Task.Delay(100);
                ChangeCharacterPlace();

                twoWayAlreadySelectedIt = false;
            }
            ChangeCharacterPlace();

        }
        public static async Task PlayRound()
        {
            IMiniGame miniGame = GetGame(SelectedGameGrid.GridType);

            miniGame.ShowDialog();
            if (miniGame.GameEndHandler.Message != "") MainWindow.AddFeedbackText(miniGame.GameEndHandler.Message);
            await TakeSteps(miniGame.GameEndHandler);
        }
        public static async Task PlayGame()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Timer_Tick;
            Timer.Start();

            SubTimer.AutoReset = true;
            SubTimer.Start();

            while (!SelectedGameGrid.IsEnd)
            {
                await PlayRound();
            }
        }

        private static void SubTimer_Tick(object? sender, ElapsedEventArgs e)
        {
            ShowSelectableGameGrids = !ShowSelectableGameGrids;
            if (!DoBlinking) Application.Current?.Dispatcher.Invoke(() =>
            {
                subGameGrids.ForEach(g => g.Rectangle.Fill = ShowSelectableGameGrids ? GetBrush(g.GridType) : new SolidColorBrush(Color.FromRgb(110, 110, 110)));
            });
        }

        private static void Timer_Tick(object? sender, EventArgs e)
        {
            ShowSelectedGameGrid = !ShowSelectedGameGrid && DoBlinking;
            Application.Current?.Dispatcher.Invoke(() => SelectedGameGrid.Rectangle.Fill = ShowSelectedGameGrid ? GetBrush(SelectedGameGrid.GridType) : GetBrush((GameGridType)(100)));
        }
    }
}
