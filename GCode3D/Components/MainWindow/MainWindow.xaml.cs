using System.Windows;
using System.Windows.Input;

namespace GCode3D.Components
{
    public partial class MainWindowControl : Window
    {
        private new MainWindowComponent? DataContext =>
            (MainWindowComponent)base.DataContext;

        public MainWindowControl() =>
            InitializeComponent();

        private void AppMinimize_Click(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;

        private void AppMaximize_Click(object sender, RoutedEventArgs e) =>
            WindowState = 
                WindowState == WindowState.Maximized ?
                    WindowState.Normal :
                    WindowState.Maximized;

        private void AppClose_Click(object sender, RoutedEventArgs e) =>
            Application.Current?.Shutdown();

        private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ToggleSettings_Click(object sender, RoutedEventArgs e)
            {}

        private void ToggleUser_Click(object sender, RoutedEventArgs e)
            {}

        private void ToggleInfo_Click(object sender, RoutedEventArgs e)
            {}
    }
}