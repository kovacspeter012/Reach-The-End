using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    class MineGameGrid
    {
        public Label Label { get; private set; }
        public int Index { get; private set; }
        public bool IsBomb { get; set; } = false;
        public bool IsRevealed { get; set; } = false;

        public MineGameGrid(Label label, int index)
        {
            Label = label;
            Index = index;
        }
    }

    static class MineGameLogic
    {
        public static void GenerateBombs(MineGameGrid[] mineGameGrids, List<MineGameGrid> bombs, int clickIndex)
        {
            //TODO
        }
    }
}
