using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    public struct GameEndHandler(bool win, bool requireDiceAfter, int extraSteps, double diceMultiplyer)
    {
        public bool Win { get; set; } = win;
        public bool RequireDiceAfter { get; set; } = requireDiceAfter;
        public int ExtraSteps { get; set; } = extraSteps;
        public double DiceMultiplyer { get; set; } = diceMultiplyer;
    }

    public interface IMiniGame
    {
        public GameEndHandler GameEndHandler { get; set; }
    }
}
