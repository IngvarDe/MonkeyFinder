using MonkeyFinder.Services;
using MonkeyFinder.ViewModel;

namespace MonkeyFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MonkeyService>();
		builder.Services.AddSingleton<MonkeysViewModel>();
		builder.Services.AddSingleton<MainPage>();

        //https://www.youtube.com/watch?v=DuNLR_NJv8U&t=3042s 1.55.45

        return builder.Build();
	}
}
