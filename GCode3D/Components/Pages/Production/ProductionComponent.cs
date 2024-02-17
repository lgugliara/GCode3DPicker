using CommunityToolkit.Mvvm.Input;

namespace GCode3D.Components
{
    public class ProductionPage : Use<ProductionPage>, IPage
    {
        #region Properties

        public PickerComponent? PickerComponent { get; set; } = 
            new Use<PickerComponent>(new());

        public PreviewComponent? PreviewComponent { get; set; } = 
            new Use<PreviewComponent>(new());

        #endregion

        #region Getters
        
        public string Name =>
            "Select program";

        #endregion

        #region Contstructor

        public ProductionPage()
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