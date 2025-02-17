using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    public struct GameEndHandler(bool win = true, bool requireDiceAfter = true, int extraSteps = 0, double diceMultiplyer = 1.0, bool twoWays = false)
    {
        public bool Win { get; set; } = win;
        public bool RequireDiceAfter { get; set; } = requireDiceAfter;
        public int ExtraSteps { get; set; } = extraSteps;
        public double DiceMultiplyer { get; set; } = diceMultiplyer;
        public bool TwoWays { get; set; } = twoWays;
    }

    public interface IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }
        public bool? ShowDialog(); // imitált || igazi
    }

    public abstract class FakeWindow
    {
        public bool? ShowDialog()
        {
            return true;
        }
    }

    public class GridBlank : FakeWindow, IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }


        public GridBlank()
        {
            GameEndHandler = new GameEndHandler() { Win = true, RequireDiceAfter = true, DiceMultiplyer = 1.0, ExtraSteps = 0, TwoWays = false };
        }
    }
    public class GridBackwards : FakeWindow, IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }


        public GridBackwards()
        {
            GameEndHandler = new GameEndHandler() { Win = false, RequireDiceAfter = true, DiceMultiplyer = 1.0, ExtraSteps = 0, TwoWays = false };
        }
    }
    public class GridDouble : FakeWindow, IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }


        public GridDouble()
        {
            GameEndHandler = new GameEndHandler() { Win = true, RequireDiceAfter = true, DiceMultiplyer = 2.0, ExtraSteps = 0, TwoWays = false };
        }
    }
    public class GridChoose : FakeWindow, IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }


        public GridChoose()
        {
            GameEndHandler = new GameEndHandler() { Win = true, RequireDiceAfter = true, DiceMultiplyer = 1.0, ExtraSteps = 0, TwoWays = true };
        }
    }
}
