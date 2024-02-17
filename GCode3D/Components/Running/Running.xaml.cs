using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class RunningControl : UserControl
    {
        private new RunningComponent? DataContext =>
            (RunningComponent)base.DataContext;

        public RunningControl() =>
            InitializeComponent();

        private void RunToggle_Click(object sender, RoutedEventArgs e) =>
            DataContext?.ToggleRun();

        private void ExpandToggle_Click(object sender, RoutedEventArgs e) =>
            DataContext?.ToggleExpand();
    }
}