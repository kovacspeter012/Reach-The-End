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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReachTheEndGame
{
    /// <summary>
    /// Interaction logic for SignsUC.xaml
    /// </summary>
    public partial class SignsUC : UserControl
    {
        public static readonly DependencyProperty TextProp = DependencyProperty.Register("Text",typeof(string), typeof(SignsUC), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SourceProp = DependencyProperty.Register("Source",typeof(ImageSource), typeof(SignsUC), new PropertyMetadata(null));
        public string Text { get => (string)GetValue(TextProp); set => SetValue(TextProp, value); }
        public ImageSource Source { get => (ImageSource)GetValue(SourceProp); set => SetValue(SourceProp, value); }
        public SignsUC()
        {
            InitializeComponent();
        }
    }
}
