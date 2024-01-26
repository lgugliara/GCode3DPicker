namespace GCode3D.ViewModels
{
    public class AppViewModel : StandardViewModel
    {
        private PickerViewModel _PickerViewModel = new();
        public PickerViewModel PickerViewModel
        {
            get => _PickerViewModel;
            set => Set(ref _PickerViewModel, value);
        }
        
        private PreviewViewModel _PreviewViewModel = new();
        public PreviewViewModel PreviewViewModel
        {
            get => _PreviewViewModel;
            set => Set(ref _PreviewViewModel, value);
        }
        
        private RunningViewModel _RunningViewModel = new();
        public RunningViewModel RunningViewModel
        {
            get => _RunningViewModel;
            set => Set(ref _RunningViewModel, value);
        }
    }
}