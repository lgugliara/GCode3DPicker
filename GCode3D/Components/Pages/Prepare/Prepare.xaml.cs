using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class PrepareControl : UserControl
    {
        private new PreparePage? DataContext =>
            (PreparePage)base.DataContext;

        public PrepareControl() =>
            InitializeComponent();
    }
}