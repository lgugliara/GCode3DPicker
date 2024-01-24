using SharpDX;

namespace GCode3D.Models
{
    public class StatelessCommand
    {
        public bool IsCompleted { get; set; } = false;
        public bool IsRunning { get; set; } = false;

        public Vector3 From { get; set; }
        public Vector3 To { get; set; }
    }
}
