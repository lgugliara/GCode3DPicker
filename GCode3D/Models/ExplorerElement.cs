namespace GCode3D.Models
{
    public enum ExplorerElementType
    {
        File = 0,
        Folder
    }

    public class ExplorerElement
    {
        public ExplorerElementType Type { get; set; }

        private string _Path = string.Empty;
        public string Path
        {
            get => _Path.Replace("\\", "/");
            set => _Path = (value ?? string.Empty).Replace("\\", "/");
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

        public bool IsRoot
        {
            get => Type == ExplorerElementType.Folder && Path.LastIndexOf('/') < 0;
        }
        public ExplorerElement PreviousFolder
        {
            get => IsRoot ? this : new ExplorerElement
                {
                    Path = Path[..Path.LastIndexOf('/')],
                    Type = ExplorerElementType.Folder
                };
        }
    }
}
