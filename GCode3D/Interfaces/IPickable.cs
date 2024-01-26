namespace GCode3D.Models.Picker
{
    public interface IPickable
    {
        public string Path { get; set; }
        public PickableType Type { get; set; }

        public string Filename { get; }
        public bool IsRoot { get; }
        public Folder? Parent { get; }

        public static IPickable From(string path, PickableType type) =>
            new Pickable { Path = path, Type = type };
    }
}