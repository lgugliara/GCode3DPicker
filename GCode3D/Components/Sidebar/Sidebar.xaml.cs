using System.Windows.Controls;

namespace GCode3D.Components
{
    public partial class SidebarControl : UserControl
    {
        private new SidebarComponent? DataContext =>
            (SidebarComponent)base.DataContext;

        public SidebarControl()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sidebar.SelectedItem is UserControl page)
                DataContext?.OnSelect?.Execute(page);
        }
    }
}