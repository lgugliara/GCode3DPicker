using GCode3D.Components;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class StatelessCommand : StandardComponent
    {
        private bool _IsCompleted = false;
        public bool IsCompleted
        {
            get => _IsCompleted;
            set => Set(ref _IsCompleted, value);
        }

        private bool _IsRunning = false;
        public bool IsRunning
        {
            get => _IsRunning;
            set => Set(ref _IsRunning, value);
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

        private string _Code = string.Empty;
        public string Code
        {
            get => _Code;
            set => Set(ref _Code, value);
        }

        private string _CommandCode = string.Empty;
        public string CommandCode
        {
            get => _CommandCode;
            set => Set(ref _CommandCode, value);
        }
    }
}
