using HelixToolkit.SharpDX.Core;
using System.Windows;
using SharpDX;
using GCode3D.Models.Program;
using HelixToolkit.Wpf.SharpDX;
using System.Windows.Media.Media3D;

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
        public HelixToolkit.Wpf.SharpDX.Camera Camera { get; } =
            new HelixToolkit.Wpf.SharpDX.PerspectiveCamera
            {
                FarPlaneDistance = 100000,
                NearPlaneDistance = 0.1
            };

        private LineGeometryModel3D _Preview =
            new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Blue,
                IsThrowingShadow = false,
            };
        public LineGeometryModel3D Preview
        {
            get => _Preview;
            set => Set(ref _Preview, value);
        }

        private LineGeometryModel3D _Pivot =
            new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Red,
                IsThrowingShadow = false,
                Geometry = CreatePivot().ToLineGeometry3D(),
            };
        public LineGeometryModel3D Pivot
        {
            get => _Pivot;
            set => Set(ref _Pivot, value);
        }

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

        public void Refresh()
        {
            #region DEV
            // TODO: Not working. The pivot is not updating
            // Moreover, the pivot is not designed to be instanced multiple times
            
            /* var position = Current?.CurrentPosition ?? Vector3.Zero;
            
            Application.Current.Dispatcher.Invoke(() =>
                Pivot = new()
                {
                    Thickness = 1,
                    Smoothness = 2,
                    Color = System.Windows.Media.Colors.Red,
                    IsThrowingShadow = false,
                    Geometry = CreatePivot().ToLineGeometry3D(),
                    Transform = new TranslateTransform3D(position.ToVector3D()),
                }
            ); */
            #endregion
        }

        #region IDisposable
        public override void Dispose()
        {

        }
        #endregion
    }
}