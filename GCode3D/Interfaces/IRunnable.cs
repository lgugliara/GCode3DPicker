// I want interfaces to be in the same folder as models
// so that are easy to find and use.

using System.Windows.Input;

namespace GCode3D.Models
{
    public interface IRunnable
    {
        public bool IsRunning { get; }

        public void Start(ICommand? onUpdate = null);
        public void Stop();
    }
}
