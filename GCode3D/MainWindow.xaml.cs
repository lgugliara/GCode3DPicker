using System.Windows;
using System.Windows.Controls;
using GCode3D.Models.Picker;
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (programsListBox.SelectedItem is not Pickable selected)
                return;

            // If the selected item is a folder, update the current folder
            if (selected.Type == PickableType.Folder)
                AppViewModel.PickerViewModel.LoadFolder(selected as Folder);
            // If the selected item is a file, update the current file
            else
                AppViewModel.PickerViewModel.LoadProgram(selected as File);
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e) =>
            AppViewModel.PickerViewModel.LoadFolder(AppViewModel.PickerViewModel.Current.Selection?.Parent as Folder);

        private void EditProgram_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RunToggle_Click(object sender, RoutedEventArgs e)
        {
            if (AppViewModel.RunningViewModel.Current == null)
                return;

            if (AppViewModel.RunningViewModel.Current.IsRunning)
                AppViewModel.RunningViewModel.Current.Stop();
            else
                AppViewModel.RunningViewModel.LoadRun();
        }

        private void AppClose_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}