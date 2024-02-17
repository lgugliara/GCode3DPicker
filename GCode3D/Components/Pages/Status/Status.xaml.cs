using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class StatusControl : UserControl
    {
        private new StatusPage? DataContext =>
            (StatusPage)base.DataContext;

        public StatusControl() =>
            InitializeComponent();
    }
}