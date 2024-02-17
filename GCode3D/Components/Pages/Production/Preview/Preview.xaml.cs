using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class PreviewControl : UserControl
    {
        private new PreviewComponent? DataContext =>
            (PreviewComponent)base.DataContext;

        public PreviewControl() => 
            InitializeComponent();
    }
}