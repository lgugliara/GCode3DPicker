using System.Windows;

namespace GCode3D.Components
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowControl : Window
    {
        private MainWindowComponent MainWindowComponent { get; set; } = new();

        public MainWindowControl()
        {
            InitializeComponent();
            DataContext = MainWindowComponent;
        }

        private void AppClose_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}