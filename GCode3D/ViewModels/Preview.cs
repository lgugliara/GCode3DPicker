using HelixToolkit.SharpDX.Core;
using GCode3D.Models.Program;
using HelixToolkit.Wpf.SharpDX;

namespace GCode3D.ViewModels
{
    public class PreviewViewModel : StandardViewModel
    {
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

        #region IDisposable
        public override void Dispose()
        {

        }
        #endregion
    }
}