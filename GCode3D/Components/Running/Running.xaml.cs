using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class RunningControl : UserControl
    {
        private RunningComponent? VM
        {
            get => DataContext as RunningComponent;
            set => DataContext = value;
        }

        public RunningControl()
        {
            InitializeComponent();
        }

        private void RunToggle_Click(object sender, RoutedEventArgs e) =>
            VM?.ToggleRun();
    }
}