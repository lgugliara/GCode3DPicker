namespace GCode3D.ViewModels
{
    public class AppViewModel : StandardViewModel
    {
        public PickerViewModel PickerViewModel { get; } = new();
        public PreviewViewModel PreviewViewModel { get; } = new();
        public RunningViewModel RunningViewModel { get; } = new();
    }
}