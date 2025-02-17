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


using static ReachTheEndGame.Table;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool AutoScroll = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => Die._s = FindResource("DieButtonStyle") as Style;
            Loaded += (s, e) => Table._s = FindResource("GameGridStyle") as Style;

            Die.DieRect = rtgDie;
            rtgDie.Loaded += (s, e) => Die.CenterDie(cnvDie, rtgDie);
            rtgDie.Loaded += (s, e) => Die.GenerateDieButtons(cnvDie);
            rtgDie.Loaded += (s, e) => Die.Throw();
            rtgDie.Loaded += (s, e) => Die.DisplayDie(rtgDie, Die.DieNumber);

            scvFeedback.ScrollChanged += this.SetFeedbackAutoScroll;

            rtgDie.PreviewMouseDown += (s, e) => Die.Throw();
            rtgDie.PreviewMouseDown += (s, e) => Die.DisplayDie(rtgDie,Die.DieNumber);

            cnvGame.Loaded += (s, e) => Table.GenerateTable(cnvGame);
            cnvGame.Loaded += async (s, e) =>
            {
                await Table.PlayGame();
                AddFeedbackText("Gratulálok, nyertél!!");
                MessageBox.Show("Gratulálok, nyertél!!", "Nyertél!", MessageBoxButton.OK);
            };

            
        }
        public void SetFeedbackAutoScroll(object s, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange == 0)
            {
                AutoScroll = scvFeedback.VerticalOffset == scvFeedback.ScrollableHeight;
            }

            if (AutoScroll && e.ExtentHeightChange != 0)
            {
                scvFeedback.ScrollToVerticalOffset(scvFeedback.ExtentHeight);
            }
        }
        public void AddFeedbackText(string text)
        {
            TextBlock l = new TextBlock() { Text = text, TextWrapping = TextWrapping.Wrap, Padding=new(3) };
            DockPanel.SetDock(l, Dock.Top);
            dcpFeedback.Children.Add(l);
        }

    }
}