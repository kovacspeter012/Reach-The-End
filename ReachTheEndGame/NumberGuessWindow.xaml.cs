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
    /// Interaction logic for NumberGuessWindow.xaml
    /// </summary>
    public partial class NumberGuessWindow : Window, IMiniGame
    {
        NumberGuess GuessedNumber = new NumberGuess(new Random().Next(1, 101));
        public GameEndHandler GameEndHandler { get; set; } = new GameEndHandler(false, false, 6, 1, false, "Kiléptél a játékból, ezért hat mezővel hátrébb fogsz menni.");
        public NumberGuessWindow()
        {
            InitializeComponent();

        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            string feedback;
            if (int.TryParse(tbGuess.Text, out int num))
            {
                feedback = GuessedNumber.NumGuess(int.Parse(tbGuess.Text));
            }
            else
            {
                feedback = "";
            }
            tbGuess.Text = null;

            if (feedback == ">")
            {
                lblGuess.Content = "A szám nagyobb!";
            }
            else if (feedback == "<")
            {
                lblGuess.Content = "A szám kisebb!";
            }
            else if (feedback == "win")
            {
                lblGuess.Content = "";
                EndGame(true, true, 0, 1);
                window.Close();
            }
            else if (feedback == "lose")
            {
                lblGuess.Content = "";
                EndGame(false, true, 0, 1);
                window.Close();
            }
            else if(feedback == "")
            {
                lblGuess.Content = "Írj be egy számot!";
            }
        }

        private void EndGame(bool win, bool requireDiceAfter, int extraSteps, double diceMultiplyer)
        {
            GameEndHandler = new(win, requireDiceAfter, extraSteps, diceMultiplyer, false, win ? "Kitaláltad a számot! Dobj a kockával a továbbhaladáshoz!" : "Kitaláltad a számot, de sajnos túl sok lépésből. A kockadobás után visszafelé fogsz lépni.");
            window.Close();
        }

        private void tbGuess_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
