using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GCode3D.Models;

namespace GCode3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (programsListBox.SelectedItem is not ExplorerElement selected)
                return;

            // If the selected item is a folder, update the current folder
            if (selected.Type == ExplorerElementType.Folder)
                viewModel.LoadFolder(selected.Path);
            // If the selected item is a file, update the current file
            else
                viewModel.LoadProgram(selected.Path);
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LoadFolder();
        }

        private void EditProgram_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RunToggle_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Program.IsRunning)
                viewModel.Program.Stop();
            else
                viewModel.RunProgram();
        }

        private void AppClose_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}