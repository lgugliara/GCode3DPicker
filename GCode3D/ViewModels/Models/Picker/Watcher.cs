using System.IO;

namespace GCode3D.ViewModels.Models.Picker
{
    public class Watcher : FileSystemWatcher
    {
        public new string Path
        {
            get => base.Path;
            set
            {
                base.Path = value;

                if (!string.IsNullOrEmpty(base.Path))
                    BeginInit();
            }
        }

        public Watcher() : base()
        {
            Path = GCode3D.Models.Picker.Picker.DefaultPath;
            Filter = "*.gcode";
            EnableRaisingEvents = true;
            NotifyFilter = NotifyFilters.Attributes
                         | NotifyFilters.CreationTime
                         | NotifyFilters.DirectoryName
                         | NotifyFilters.FileName
                         | NotifyFilters.LastAccess
                         | NotifyFilters.LastWrite
                         | NotifyFilters.Security
                         | NotifyFilters.Size;
        }
    };
}