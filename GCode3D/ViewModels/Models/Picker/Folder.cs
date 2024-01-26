namespace GCode3D.Models.Picker
{

    public class Folder : Pickable
    {
        public override PickableType Type { get; set; } = PickableType.Folder;
    }
}
