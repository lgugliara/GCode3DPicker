using System.IO;
using GCode3D.ViewModels.Models.Picker;

namespace GCode3D.Models.Picker
{
    public class Picker
    {
        public static readonly string DefaultPath = @"C:/GCode3DPicker/GCode3D/resources/gcodes";

        private IPickable _Location = new Folder() { Path = DefaultPath };
        public IPickable Location
        {
            get => _Location;
            set
            {
                _Location = value;
                Watcher.Path = value.Path;
            }
        }

        public IPickable? Selection { get; set; }

        public Watcher Watcher { get; private set; } = new();

        public IEnumerable<IPickable> Folders
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

        public IEnumerable<IPickable> Files
        {
            get => [
                .. Directory.GetFiles(Location.Path)
                .Select(filename => new Pickable
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
