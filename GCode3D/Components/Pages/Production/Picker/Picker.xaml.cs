using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class PickerControl : UserControl
    {
        private new PickerComponent? DataContext =>
            (PickerComponent)base.DataContext;

        public PickerControl() =>
            InitializeComponent();

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (picker.SelectedItem is FileSystemInfo selected)
                DataContext?.Select(selected);
        }

        private void PickerBack_Click(object sender, RoutedEventArgs e) =>
            DataContext?.Back();

        private void EditProgram_Click(object sender, RoutedEventArgs e) {}
    }
}