namespace GCode3D.Models.Picker
{
    public class Pickable : IPickable
    {
        private string _Path = string.Empty;
        public string Path
        {
            get => _Path.Replace("\\", "/");
            set => _Path = (value ?? string.Empty).Replace("\\", "/");
        }
        
        public virtual PickableType Type { get; set; }

        public string Filename
        {
            get =>
                Type == PickableType.Folder ?
                    Path.Split("/").LastOrDefault() ?? string.Empty :
                    Path.Split("/").LastOrDefault() ?? string.Empty;
        }

        public bool IsRoot {
            get => Type == PickableType.Folder && Path.LastIndexOf('/') < 0;
        }
        
        public IPickable Parent
        {
            get => IsRoot ? 
                this :
                new() {
                    Path = Path[..Path.LastIndexOf('/')],
                    Type = PickableType.Folder
                };
        }
    }
}
