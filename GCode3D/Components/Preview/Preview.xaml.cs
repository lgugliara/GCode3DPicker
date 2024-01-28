using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class PreviewControl : UserControl
    {
        private PreviewComponent? VM
        {
            get => DataContext as PreviewComponent;
            set => DataContext = value;
        }

        public PreviewControl()
        {
            InitializeComponent();
        }
    }
}