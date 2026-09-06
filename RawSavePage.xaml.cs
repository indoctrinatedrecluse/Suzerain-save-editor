namespace SuzerainSaveEditor;

public partial class RawSavePage : ContentPage
{
    public RawSavePage(RawSaveViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
