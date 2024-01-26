using System.IO;
using GCode3D.Models;
using GCode3D.Models.Picker;

namespace GCode3D.ViewModels
{
    public class PickerViewModel : StandardViewModel
    {
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

        public async Task<IEnumerable<StatelessCommand>> LoadProgram(GCode3D.Models.Picker.File? from = null) =>
            await Task.Run(() =>
                {
                    // TODO: Handle with exceptions
                    if (Current == null)
                        return [];

                    Current.Selection = from;
                    return Parser.From(Current.Selection);
                }
            );

        public async Task LoadFolder(Folder? from = null) =>
            await Task.Run(() =>
                {
                    // TODO: Handle with exceptions (ArgumentNullException)
                    if (from == null)
                        return;

                    if (Current == null)
                        return;
                    
                    Current.Location = from;
                    OnPropertyChanged(nameof(Current));
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