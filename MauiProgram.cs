using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace SuzerainSaveEditor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<BaseGamePage>();
        builder.Services.AddSingleton<BaseGameViewModel>();
        
        builder.Services.AddSingleton<RiziaPage>();
        builder.Services.AddSingleton<RiziaViewModel>();

		return builder.Build();
	}
}