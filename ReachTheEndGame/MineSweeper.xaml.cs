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
        bool IsGameStarted { get; set; } = false;
        MineGameGrid[] MineGameGrids;
        public MineSweeper()
        {
            Bombs = new();
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

                    MineGameGrids[i*8+j] = new MineGameGrid(l,i*8+j);

                    l.PreviewMouseDown += lblClick;
                }
            }
        }

        private void lblClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Label) return;
            Label label = (Label)sender;

            MineGameGrid? mineGameGrid = MineGameGrids.FirstOrDefault(e=>e.Label == label);
            if (mineGameGrid is null) return;

            if (IsGameStarted)
            {
                
            }
            else
            {
                MineGameLogic.GenerateBombs(MineGameGrids,Bombs,mineGameGrid.Index);
            }
        }

    }
}
