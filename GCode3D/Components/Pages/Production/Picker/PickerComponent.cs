using System.IO;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Picker;

namespace GCode3D.Components
{
    public class PickerComponent : Use<PickerComponent>
    {
        #region Commands

        public IRelayCommand? OnSelect { get; set; }

        #endregion

        #region Properties

        public Picker? Current { get; set; } = 
            new Use<Picker>(new Picker());

        #endregion

        #region Methods

        public void Select(FileSystemInfo? from = null)
        {
            // TODO: Handle with exceptions (ArgumentNullException)
            if (from == null)
                return;

            if (Current == null)
                return;
                    
            if(from is DirectoryInfo)
                Current.Location = from as DirectoryInfo;
            else if(from is FileInfo)
            {
                Current.Selection = from as FileInfo;
                OnSelect?.Execute(null);
            }
        }

        public void Back()
        {
            if(Current == null)
                return;

            // TODO: Handle with exceptions (ArgumentNullException)
            if (Current.Location?.Parent is not DirectoryInfo back)
                return;

            if (Current.Location?.ToString() == back.ToString())
                return;
                    
            Current.Location = back;
        }

        #endregion

        #region IDisposable

        public override void Dispose() =>
            Current?.Dispose();

        #endregion
    }
}