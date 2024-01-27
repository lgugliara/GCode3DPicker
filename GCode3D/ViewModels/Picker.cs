using System.IO;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Picker;

namespace GCode3D.ViewModels
{
    public class PickerViewModel : StandardViewModel
    {
        public IRelayCommand? OnSelect { get; set; }

        private Picker? _Current = new();
        public Picker? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }

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

        public async Task Back() =>
            await Task.Run(() =>
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
            );

        #region IDisposable
        public override void Dispose() =>
            Current?.Dispose();
        #endregion
    }
}