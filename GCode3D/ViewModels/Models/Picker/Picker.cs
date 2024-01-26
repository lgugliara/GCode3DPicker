using System.IO;
using Avalonia.Markup.Xaml.MarkupExtensions;
using GCode3D.ViewModels;
using GCode3D.ViewModels.Models.Picker;

namespace GCode3D.Models.Picker
{
    public class Picker : StandardViewModel
    {
        public static readonly string DefaultPath = @"C:/GCode3DPicker/GCode3D/resources/gcodes";

        private IPickable _Location = new Folder() { Path = DefaultPath };
        public IPickable Location
        {
            get => _Location;
            set
            {
                Watcher.Path = value.Path;
                Set(ref _Location, value);
                OnPropertyChanged(nameof(Content));
            }
        }

        private IPickable? _Selection;
        public IPickable? Selection
        {
            get => _Selection;
            set => Set(ref _Selection, value);
        }

        public Watcher Watcher { get; private set; } = new();

        public IEnumerable<Folder> Folders
        {
            get => [
                .. Directory.GetDirectories(Location.Path, "*", SearchOption.AllDirectories)
                .Select(filename => new Folder
                {
                    Path = filename
                })
                .OrderBy(e => e.Filename)
            ];
        }

        public IEnumerable<File> Files
        {
            get => [
                .. Directory.GetFiles(Location.Path)
                .Select(filename => new File
                {
                    Path = filename,
                    Type = PickableType.File
                })
                .OrderBy(e => e.Filename)
            ];
        }

        public IEnumerable<IPickable> Content
        {
            get => [.. Folders, .. Files];
        }
    }
}
