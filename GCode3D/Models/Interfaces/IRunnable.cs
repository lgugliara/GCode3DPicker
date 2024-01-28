using System.Windows.Input;

namespace GCode3D.Models.Interfaces
{
    public interface IRunnable
    {
        public bool IsRunning { get; }

        public void Start(ICommand? onUpdate = null);
        public void Stop();
    }
}
