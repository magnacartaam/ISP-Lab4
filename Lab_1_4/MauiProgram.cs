using _253504_Patrebka_23;
using Microsoft.Extensions.Logging;

namespace _253504_Patrebka_23;

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
		
		builder.Services.AddHttpClient<IRateService, RateService>(opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"));
		builder.Services.AddTransient<CurrencyConverter>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
