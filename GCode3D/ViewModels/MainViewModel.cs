using HelixToolkit.SharpDX.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HelixToolkit.Wpf.SharpDX;
using GCode3D.Models;
using System.IO;
using System.Windows;
using System.Diagnostics;
using SharpDX;

namespace GCode3D
{
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        public GCPicker Picker { get; } = new GCPicker();
        public GCProgram Program { get; set; }

        public EffectsManager EffectsManager { get; } = new DefaultEffectsManager();
        public Camera Camera { get; } = new PerspectiveCamera{ FarPlaneDistance = 100000, NearPlaneDistance = 0.1 };
	    public LineGeometryModel3D GCMesh { get; } = new LineGeometryModel3D()
        {
            Thickness = 1,
            Smoothness = 2,
            Color = System.Windows.Media.Colors.Blue,
            IsThrowingShadow = false,
        };
        public LineGeometryModel3D GCPivot { get; } = new LineGeometryModel3D()
        {
            Thickness = 1,
            Smoothness = 2,
            Color = System.Windows.Media.Colors.Red,
            IsThrowingShadow = false,
            Geometry = CreateArrowGeometry().ToLineGeometry3D(),
        };

        private static LineBuilder CreateArrowGeometry()
        {
            var g = new LineBuilder();
            g.AddLine(Vector3.Right, Vector3.Left);
            g.AddLine(Vector3.Down, Vector3.Up);
            g.AddLine(Vector3.BackwardLH, Vector3.ForwardLH);
            return g;
        }

        public MainViewModel()
        {
            // Subscribe to events
            Picker.Watcher.Renamed += LoadWatcher;
            Picker.Watcher.Created += LoadWatcher;
            Picker.Watcher.Deleted += LoadWatcher;
            Picker.Watcher.Changed += LoadWatcher;
        }

        public void RunProgram()
        {
            Program.Start((StatelessCommand command) => {
                OnPropertyChanged(nameof(Program));
                OnPropertyChanged(nameof(GCPivot));
            });
        }
        
        public void LoadProgram(string to)
        {
            var currentElement = new ExplorerElement()
            {
                Path = to,
                Type = ExplorerElementType.File
            };

            Task.Run(() =>
            {
                Program?.Stop();
                // Load the program from the file
                Picker.CurrentFile = currentElement;
                Program = GCodeParser.ParseFile(currentElement);

                // Update the mesh with the new program data
                var geometry = Program.ToLineBuilder().ToLineGeometry3D();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    GCMesh.Geometry = geometry;
                    OnPropertyChanged(nameof(Program));
                    OnPropertyChanged(nameof(GCMesh));
                });
            });
        }

        public void LoadFolder(string? to = null)
        {
            Picker.CurrentFolder = string.IsNullOrEmpty(to) ?
                Picker.CurrentFolder.PreviousFolder : 
                new()
                {
                    Path = to,
                    Type = ExplorerElementType.Folder
                };

            OnPropertyChanged(nameof(Picker));
        }

        public void LoadWatcher(object sender, FileSystemEventArgs e)
        {
            OnPropertyChanged(nameof(Picker));
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
            Picker.Watcher.Dispose();
        }
        #endregion
    }
}