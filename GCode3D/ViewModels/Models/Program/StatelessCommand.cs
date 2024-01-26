using GCode3D.ViewModels;
using SharpDX;

namespace GCode3D.Models
{
    public class StatelessCommand : StandardViewModel
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
    }
}
