using System.Windows;
using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class OutputLoggerControl : UserControl
    {
        private new OutputLoggerComponent? DataContext =>
            (OutputLoggerComponent)base.DataContext;

        public OutputLoggerControl() => 
            InitializeComponent();

        private void LoggerList_LayoutUpdated(object sender, EventArgs e) =>
            LoggerList.ScrollIntoView(DataContext?.Current?.LastOrDefault());
    }
}