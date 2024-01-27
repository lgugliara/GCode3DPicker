using System.IO;
using System.Windows;
using System.Windows.Controls;
using GCode3D.ViewModels;

namespace GCode3D.Views
{
    public partial class Picker : UserControl
    {
        private PickerViewModel? VM
        {
            get => DataContext as PickerViewModel;
            set => DataContext = value;
        }

        public Picker()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (programsListBox.SelectedItem is not FileSystemInfo selected)
                return;
            
            VM?.Select(selected);      
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e) =>
            VM?.Back();

        private void EditProgram_Click(object sender, RoutedEventArgs e) {}
    }
}