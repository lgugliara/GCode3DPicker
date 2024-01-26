using System.Windows.Controls;
using GCode3D.ViewModels;

namespace GCode3D.Views
{
    public partial class Preview : UserControl
    {
        private PreviewViewModel? VM
        {
            get => DataContext as PreviewViewModel;
            set => DataContext = value;
        }

        public Preview()
        {
            InitializeComponent();
        }
    }
}