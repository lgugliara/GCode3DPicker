using System.Windows;
using System.Windows.Controls;
using GCode3D.ViewModels;

namespace GCode3D.Views
{
    public partial class Running : UserControl
    {
        private RunningViewModel? VM
        {
            get => DataContext as RunningViewModel;
            set => DataContext = value;
        }

        public Running()
        {
            InitializeComponent();
        }

        private void RunToggle_Click(object sender, RoutedEventArgs e)
        {
            if (VM.Current == null)
                return;

            if (VM.Current.IsRunning)
                VM.Current.Stop();
            else
                VM.LoadRun();
        }
    }
}