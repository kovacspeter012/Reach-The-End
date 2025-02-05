using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => Die._s = FindResource("DieButtonStyle") as Style;
            rtgDie.Loaded += (s,e) => Die.CenterDie(cnvDie, rtgDie);
            rtgDie.Loaded += (s, e) => Die.GenerateDieButtons(cnvDie);
        }
    }
}