// I want interfaces to be in the same folder as models
// so that are easy to find and use.

namespace GCode3D.Models
{
    public interface IRunnable
    {
        public bool IsRunning { get; }
        public void Start(Delegate callback);
        public void Stop();
    }
}
