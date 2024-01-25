namespace GCode3D.Models.Interfaces
{
    public interface IGCRunnable
    {
        public bool IsRunning { get; }
        public void Start(Delegate callback);
        public void Stop();
    }
}
