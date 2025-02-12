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
    public partial class NumberGuessWindow : Window
    {
        NumberGuess GuessedNumber = new NumberGuess(new Random().Next(1, 101));
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
                MessageBox.Show("Kitaláltad a számot!");

                window.Close();
            }
            else if (feedback == "lose")
            {
                lblGuess.Content = "";
                MessageBox.Show("Kitaláltad a számot de sajnos\ntúl sok lépésből!");

                window.Close();
            }
            else if(feedback == "")
            {
                lblGuess.Content = "Írj be egy számot!";
            }
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
