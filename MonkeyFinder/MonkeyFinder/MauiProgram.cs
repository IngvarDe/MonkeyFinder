﻿using MonkeyFinder.Services;
using MonkeyFinder.View;
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
		builder.Services.AddTransient<MonkeyDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<DetailsPage>();

        //https://www.youtube.com/watch?v=DuNLR_NJv8U&t=3042s 2.44

        return builder.Build();
	}
}
