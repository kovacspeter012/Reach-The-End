using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    internal class NumberGuess
    {
        public int TheNumber { get; set; }
        public int NumberOfGuesses { get; set; }

        public NumberGuess(int rndNum)
        {
            TheNumber = rndNum;
            NumberOfGuesses = 0;
        }

        public string NumGuess(int num)
        {
            NumberOfGuesses += 1;
            if (num < TheNumber)
            {
                return ">";
            }
            else if (num > TheNumber)
            {
                return "<";
            }
            else
            {
                if (WinOrLose())
                    return "win";
                else
                    return "lose";
            }
        }

        private bool WinOrLose()
        {
            if (NumberOfGuesses <= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
