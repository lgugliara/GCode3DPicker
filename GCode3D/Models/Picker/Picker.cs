using System.IO;
using GCode3D.Components;

namespace GCode3D.Models.Picker
{
    public class Picker : Use<Picker>
    {
        private static readonly string DefaultPath = @"C:/xxx/GCode3DPicker/GCode3D/resources/gcodes";

        private DirectoryInfo? _Location = new(DefaultPath);
        public DirectoryInfo? Location
        {
            get => _Location;
            set
            {
                Set(ref _Location, value);
                OnRefresh();
            }
        }

        private FileInfo? _Selection;
        public FileInfo? Selection
        {
            get => _Selection;
            set => Set(ref _Selection, value);
        }

        private FileSystemWatcher _Watcher = 
            new()
            {
                Path = DefaultPath,
                Filter = "*.gcode",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.Attributes
                            | NotifyFilters.CreationTime
                            | NotifyFilters.DirectoryName
                            | NotifyFilters.FileName
                            | NotifyFilters.LastAccess
                            | NotifyFilters.LastWrite
                            | NotifyFilters.Security
                            | NotifyFilters.Size,
            };
        private FileSystemWatcher Watcher
        {
            get => _Watcher;
            set => Set(ref _Watcher, value);
        }

        private IEnumerable<DirectoryInfo>? Folders => 
            Location?.EnumerateDirectories()
            .Where(e => 
                (e.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden &&
                e.Name != e.Extension
            )
            .OrderBy(e => e.Name);

        private IEnumerable<FileInfo>? Files =>
            Location?.EnumerateFiles("*.gcode")
            .Where(e => 
                (e.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden &&
                e.Name != e.Extension
            )
            .OrderBy(e => e.Name);

        public IEnumerable<FileSystemInfo> Content
        {
            get => [.. Folders, .. Files];
        }

        public Picker()
        {
            Watcher.Created += (s, e) => OnRefresh();
            Watcher.Deleted += (s, e) => OnRefresh();
            Watcher.Renamed += (s, e) => OnRefresh();
            Watcher.Changed += (s, e) => OnRefresh();
        }

        private void OnRefresh()
        {
            Watcher.Path = Location?.ToString() ?? DefaultPath;
            Watcher.BeginInit();
            OnPropertyChanged(nameof(Content));
        }

        public override string ToString()
            => Path.GetFileNameWithoutExtension(Location?.Name) ?? string.Empty;
    }
}
