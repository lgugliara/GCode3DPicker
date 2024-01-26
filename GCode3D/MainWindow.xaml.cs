using System.Windows;
using GCode3D.ViewModels;

namespace GCode3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppViewModel AppViewModel { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            AppViewModel = new();
            DataContext = AppViewModel;
        }

        private void AppClose_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}