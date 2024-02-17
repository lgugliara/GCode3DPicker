using CommunityToolkit.Mvvm.Input;

namespace GCode3D.Components
{
    public class StatusPage : Use<StatusPage>, IPage
    {
        #region Properties

        public PickerComponent? PickerComponent { get; set; } = 
            new Use<PickerComponent>(new());

        public PreviewComponent? PreviewComponent { get; set; } = 
            new Use<PreviewComponent>(new());

        #endregion

        #region Getters
        
        public string Name =>
            "Status";

        #endregion

        #region Contstructor

        public StatusPage()
        {
            if(PickerComponent != null)
                PickerComponent.OnSelect =
                    new RelayCommand(() =>
                    {
                        if(
                            PickerComponent != null && 
                            PreviewComponent != null
                        )
                            PreviewComponent.Program = 
                                new()
                                {
                                    File = PickerComponent.Picker?.Selection
                                };
                    });
        }

        #endregion
    }
}