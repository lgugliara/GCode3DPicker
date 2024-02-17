using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class SelectControl : UserControl
    {
        private new SelectPage? DataContext =>
            (SelectPage)base.DataContext;

        public SelectControl() =>
            InitializeComponent();
    }
}