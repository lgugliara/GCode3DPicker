using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class ProductionControl : UserControl
    {
        private new ProductionPage? DataContext =>
            (ProductionPage)base.DataContext;

        public ProductionControl() =>
            InitializeComponent();
    }
}