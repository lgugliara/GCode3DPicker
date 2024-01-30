using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class PickerControl : UserControl
    {
        private PickerComponent? VM
        {
            get => DataContext as PickerComponent;
            set => DataContext = value;
        }

        public PickerControl()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (picker.SelectedItem is not FileSystemInfo selected)
                return;
            
            VM?.Select(selected);      
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e) =>
            VM?.Back();

        private void EditProgram_Click(object sender, RoutedEventArgs e) {}
    }
}