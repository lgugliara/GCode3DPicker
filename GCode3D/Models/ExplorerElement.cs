using System.ComponentModel;
using System.Diagnostics;

namespace GCode3D.Models
{
    public enum ExplorerElementType
    {
        File = 0,
        Folder
    }

    public class ExplorerElement : INotifyPropertyChanged
    {
        public ExplorerElementType Type { get; set; }

        private string _path = string.Empty;
        public string Path
        {
            get => _path.Replace("\\", "/");
            set
            {
                if (_path != value)
                {
                    _path = (value ?? string.Empty).Replace("\\", "/");

                    Debug.Print($"{nameof(Filename)}: {Filename}");
                    OnPropertyChanged(nameof(Path));
                    OnPropertyChanged(nameof(Filename));
                }
            }
        }

        public string Filename
        {
            get {
                if (Type == ExplorerElementType.Folder)
                    return Path.Split("/").LastOrDefault() ?? string.Empty;
                else
                    return Path.Split("/").LastOrDefault() ?? string.Empty;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
