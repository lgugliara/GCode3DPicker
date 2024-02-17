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

        public Picker? Picker { get; set; } = 
            new Use<Picker>(new Picker());

        #endregion

        #region Methods

        public void Select(FileSystemInfo? from = null)
        {
            // TODO: Handle with exceptions (ArgumentNullException)
            if (from == null)
                return;

            if (Picker == null)
                return;
                    
            if(from is DirectoryInfo)
                Picker.Location = from as DirectoryInfo;
            else if(from is FileInfo)
            {
                Picker.Selection = from as FileInfo;
                OnSelect?.Execute(null);
            }
        }

        public void Back()
        {
            if(Picker == null)
                return;

            // TODO: Handle with exceptions (ArgumentNullException)
            if (Picker.Location?.Parent is not DirectoryInfo back)
                return;

            if (Picker.Location?.ToString() == back.ToString())
                return;
                    
            Picker.Location = back;
        }

        #endregion

        #region IDisposable

        public override void Dispose() =>
            Picker?.Dispose();

        #endregion
    }
}