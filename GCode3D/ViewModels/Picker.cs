using System.IO;
using System.Windows.Input;
using GCode3D.Models.Picker;

namespace GCode3D.ViewModels
{
    public class PickerViewModel : StandardViewModel
    {
        public ICommand PreviewCommand { get; set; }

        public Picker? _Current = new();
        public Picker? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }

        public PickerViewModel()
        {
            // Attach the events to the watcher
            // (Realtime update of the picker)
            _Current.Watcher.Renamed += LoadWatcher;
            _Current.Watcher.Created += LoadWatcher;
            _Current.Watcher.Deleted += LoadWatcher;
            _Current.Watcher.Changed += LoadWatcher;
        }

        public async Task Select(IPickable? from = null) =>
            await Task.Run(() =>
                {
                    // TODO: Handle with exceptions (ArgumentNullException)
                    if (from == null)
                        return;

                    if (Current == null)
                        return;
                    
                    if(from is Folder)
                        Current.Location = from;
                    else if(from is GCode3D.Models.Picker.File)
                        Current.Selection = from;

                    PreviewCommand?.Execute(null);
                }
            );

        public async Task Back() =>
            await Task.Run(() =>
                {
                    if(Current == null)
                        return;

                    // TODO: Handle with exceptions (ArgumentNullException)
                    if (Current.Location?.Parent is not Folder back)
                        return;

                    if (Current.Location?.Path == back.Path)
                        return;
                    
                    Current.Location = back;
                }
            );

        public void LoadWatcher(object sender, FileSystemEventArgs e) =>
            OnPropertyChanged(nameof(Current));

        #region IDisposable
        public override void Dispose()
        {
            Current?.Watcher.Dispose();
        }
        #endregion
    }
}