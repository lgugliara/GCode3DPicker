using HelixToolkit.SharpDX.Core;
using System.Windows;
using SharpDX;
using GCode3D.Models.Program;
using HelixToolkit.Wpf.SharpDX;

namespace GCode3D.ViewModels
{
    public class PreviewViewModel : StandardViewModel
    {
        private static LineBuilder CreatePivot()
        {
            var g = new LineBuilder();
            g.AddLine(Vector3.Right, Vector3.Left);
            g.AddLine(Vector3.Down, Vector3.Up);
            g.AddLine(Vector3.BackwardLH, Vector3.ForwardLH);
            return g;
        }

        private Program? _Current = new();
        public Program? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }

        public EffectsManager EffectsManager { get; } =
            new DefaultEffectsManager();
        public Camera Camera { get; } =
            new PerspectiveCamera
            {
                FarPlaneDistance = 100000,
                NearPlaneDistance = 0.1
            };

        public LineGeometryModel3D Mesh { get; } =
            new LineGeometryModel3D()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Blue,
                IsThrowingShadow = false,
            };

        public LineGeometryModel3D Pivot { get; } =
            new LineGeometryModel3D()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Red,
                IsThrowingShadow = false,
                Geometry = CreatePivot().ToLineGeometry3D(),
            };

        public async Task LoadProgram()
                {
            if(Current == null)
                return;

                    // Update the mesh with the new program data
            var geometry = (await Current.ToLineBuilder()).ToLineGeometry3D();

                    Application.Current.Dispatcher.Invoke(() =>
                Preview = new()
                {
                    Thickness = 1,
                    Smoothness = 2,
                    Color = System.Windows.Media.Colors.Blue,
                    IsThrowingShadow = false,
                    Geometry = geometry,
                }
            );
        }

        #region IDisposable
        public override void Dispose()
        {

        }
        #endregion
    }
}