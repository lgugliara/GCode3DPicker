namespace GCode3D.Models.Picker
{
    public class File : Pickable
    {
        public override PickableType Type { get; set; } = PickableType.File;
    }
}
