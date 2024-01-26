using System.Windows;
using System.Windows.Controls;
using GCode3D.Models.Picker;
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
            if (programsListBox.SelectedItem is not Pickable selected)
                return;

            // If the selected item is a folder, update the current folder
            if (selected.Type == PickableType.Folder)
                VM.LoadFolder(selected as Folder);
            // If the selected item is a file, update the current file
            else
                VM.LoadProgram(selected as File);
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e) =>
            VM.LoadFolder(VM.Current.Selection?.Parent as Folder);

        private void EditProgram_Click(object sender, RoutedEventArgs e) {}
    }
}