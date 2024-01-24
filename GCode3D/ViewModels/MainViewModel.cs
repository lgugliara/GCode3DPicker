using HelixToolkit.SharpDX.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HelixToolkit.Wpf.SharpDX;
using GCode3D.Models;
using System.IO;

namespace GCode3D
{
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        public GCPicker Picker { get; } = new GCPicker();
        public GCProgram Program { get; set; }

        public EffectsManager EffectsManager { get; } = new DefaultEffectsManager();
        public Camera Camera { get; } = new PerspectiveCamera{ FarPlaneDistance = 100000, NearPlaneDistance = 0.1 };
	    public LineGeometryModel3D Mesh { get; } = new LineGeometryModel3D()
        {
            Thickness = 1,
            Smoothness = 2,
            Color = System.Windows.Media.Colors.Blue,
            IsThrowingShadow = false,
        };

        public MainViewModel()
        {
            // Subscribe to events
            Picker.Watcher.Renamed += LoadWatcher;
            Picker.Watcher.Created += LoadWatcher;
            Picker.Watcher.Deleted += LoadWatcher;
            Picker.Watcher.Changed += LoadWatcher;
            
            LoadProgram();
        }
        
        public void LoadProgram()
        {
            // Load the program from the file
            Program = GCodeParser.ParseFile(Picker.CurrentFile.Path);

            // Update the mesh with the new program data
            Mesh.Geometry = Program.ToLineBuilder().ToLineGeometry3D();
            
            OnPropertyChanged(nameof(Program));
            OnPropertyChanged(nameof(Mesh));
        }

        public void NavigateBack()
        {
            Picker.CurrentFolder = new()
            {
                Path = Picker.CurrentFolder.Path[..Math.Max(Picker.CurrentFolder.Path.LastIndexOf('/'), 0)],
                Type = ExplorerElementType.Folder
            };

            OnPropertyChanged(nameof(Picker));
        }

        public void LoadWatcher(object sender, FileSystemEventArgs e)
        {
            OnPropertyChanged(nameof(Picker.Folders));
            OnPropertyChanged(nameof(Picker.Files));
        }
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        protected bool Set<T>(ref T backingField, T value, [CallerMemberName]string propertyName = "")
        {
            if (object.Equals(backingField, value))
            {
                return false;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            Program.Dispose();
            Picker.Watcher.Dispose();
        }
        #endregion
    }
}