using ImproveMe.Services;
using ImproveMe.View;
using ImproveMe.ViewModel;
using Microsoft.Extensions.Logging;

namespace ImproveMe;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif



        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();


        builder.Services.AddTransient<TaskDetailsPage>();
        builder.Services.AddTransient<TaskDetailsViewModel>();

        builder.Services.AddTransient<UserDetailsPage>();
        builder.Services.AddTransient<UserDetailsViewModel>();

        builder.Services.AddTransient<AppTaskPage>();
        builder.Services.AddTransient<AppTaskViewModel>();

        return builder.Build();
	}
}
