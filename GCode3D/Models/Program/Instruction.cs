using GCode3D.Components;
using GCode3D.Models.Interfaces;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class Instruction : StandardComponent, IInstructable
    {
        private object _OriginalValue = string.Empty;
        public virtual object OriginalValue
        {
            get => _OriginalValue;
            set => Set(ref _OriginalValue, value);
        }
        
        private Vector3 _From = new();
        public Vector3 From
        {
            get => _From;
            set => Set(ref _From, value);
        }

        private Vector3 _To = new();
        public Vector3 To
        {
            get => _To;
            set => Set(ref _To, value);
        }

        private string _Name = string.Empty;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private bool _IsRunning = false;
        public bool IsRunning
        {
            get => _IsRunning;
            set => Set(ref _IsRunning, value);
        }

        private bool _IsCompleted = false;
        public bool IsCompleted
        {
            get => _IsCompleted;
            set => Set(ref _IsCompleted, value);
        }

        public override string ToString()
        {
            return OriginalValue.ToString() ?? string.Empty;
        }
    }
}
