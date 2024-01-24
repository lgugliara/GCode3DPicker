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
            var selected = programsListBox.SelectedItem as ExplorerElement;
            if (selected == null)
                return;

            // If the selected item is a folder, update the current folder
            if(selected.Type == ExplorerElementType.Folder)
                viewModel.Picker.CurrentFolder = selected;

            // If the selected item is a file, update the current file
            else
                viewModel.Picker.CurrentFile = selected;
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NavigateBack();
        }
    }
}