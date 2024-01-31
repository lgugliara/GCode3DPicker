using HelixToolkit.SharpDX.Core;
using GCode3D.Models.Program;
using HelixToolkit.Wpf.SharpDX;
using CommunityToolkit.Mvvm.Input;
using SharpDX;

namespace GCode3D.Components
{
    public class PreviewComponent : StandardComponent
    {
        private Program? _Current = new();
        public Program? Current
        {
            get => _Current;
            set
            {
                Set(ref _Current, value);

                if(value == null)
                    return;

                value.OnUpdate = 
                    new RelayCommand(() =>
                    {
                        var targetPivotPosition = Current?.CurrentPosition ?? default;
                        var targetCameraPosition = targetPivotPosition + Vector3.One;
                            
                        Camera.Position = targetCameraPosition.ToPoint3D();
                        Camera.LookDirection = (targetPivotPosition - targetCameraPosition).ToVector3D();

                        // TODO: Update camera zoom
                        // Camera.ZoomToRectangle(Current?.Preview.Bounds...);
                        // to find the rect length, use:
                        //Camera.(
                        //    new Rect(-diagonal * 0.5, -diagonal * 0.5, diagonal, diagonal)
                        //);
                        //
                        var bounds = Current?.Preview.Bounds;
                        var diagonal = (bounds?.Maximum - bounds?.Minimum)?.Length() ?? 2;
                        //Camera.FieldOfView = Math.Atan2(diagonal / 2, boundingBox.SizeZ * 2) * (180 / Math.PI) * 2;
                    });
            }
        }

        public EffectsManager EffectsManager { get; } =
            new DefaultEffectsManager();

        private Camera _Camera = new PerspectiveCamera
            {
                FarPlaneDistance = 100000,
                NearPlaneDistance = 0.1,
            };
        public Camera Camera
        {
            get => _Camera;
            set => Set(ref _Camera, value);
        }

        #region IDisposable
        public override void Dispose()
        {

        }
        #endregion
    }
}