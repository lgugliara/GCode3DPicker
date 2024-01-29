using SharpDX;

namespace GCode3D.Models.Interfaces
{
    public interface IInstructable
    {
        public object OriginalValue { get; set; }

        public Vector3 From { get; set; }
        public Vector3 To { get; set; }

        public string Name { get; set; }
        
        public bool IsRunning { get; set; }
        public bool IsCompleted { get; set; }
    }
}
