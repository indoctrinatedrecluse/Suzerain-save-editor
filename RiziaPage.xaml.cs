namespace SuzerainSaveEditor;

public partial class RiziaPage : ContentPage
{
	public RiziaPage(RiziaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}