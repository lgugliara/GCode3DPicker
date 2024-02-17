using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;

namespace GCode3D.Components
{
    public class SidebarComponent : Use<SidebarComponent>
    {
        #region Commands

        public IRelayCommand? OnSelect { get; set; }

        #endregion

        #region Properties

        public List<UserControl>? Pages { get; set; } = 
            new Use<List<UserControl>>(
                [
                    new SelectControl(),
                    new PrepareControl(),
                    new StatusControl(),
                ]
            );

        #endregion
    }
}