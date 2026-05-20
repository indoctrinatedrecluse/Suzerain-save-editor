namespace SuzerainSaveEditor;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(BaseGamePage), typeof(BaseGamePage));
        Routing.RegisterRoute(nameof(RiziaPage), typeof(RiziaPage));
	}
}