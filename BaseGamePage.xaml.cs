namespace SuzerainSaveEditor;

public partial class BaseGamePage : ContentPage
{
	public BaseGamePage(BaseGameViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}