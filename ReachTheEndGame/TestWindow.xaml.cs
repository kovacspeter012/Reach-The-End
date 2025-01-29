using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        NumberGuess GuessedNumber = new NumberGuess(new Random().Next(1, 101));
        public TestWindow()
        {
            InitializeComponent();
            
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {

            string feedback = GuessedNumber.NumGuess(int.Parse(tbGuess.Text));
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
                lblGuess.Content = "Nyertél!";
            }
            else if (feedback == "lose")
            {
                lblGuess.Content = "Kitaláltad a számot de sajnos túl sok lépésből!";
            }
        }

        private void tbGuess_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = Regex.IsMatch(e.Text);
        }

    }
}
