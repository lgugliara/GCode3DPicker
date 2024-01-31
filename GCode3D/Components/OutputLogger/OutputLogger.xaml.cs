using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class OutputLoggerControl : UserControl
    {
        private OutputLoggerComponent? VM
        {
            get => DataContext as OutputLoggerComponent;
            set => DataContext = value;
        }

        public OutputLoggerControl()
        {
            InitializeComponent();
        }

        private void LoggerList_LayoutUpdated(object sender, EventArgs e) =>
            LoggerList.ScrollIntoView(VM?.Current?.LastOrDefault());
    }
}