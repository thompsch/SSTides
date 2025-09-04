using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace SalishSeaTides;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDAxNjU2NkAzMzMwMmUzMDJlMzAzYjMzMzAzYldRNGxrcTI2d2g1NXhDa21vNFNZQWRoTUVkOGRFcjZRV2wyYm8zMGNlUlE9");
		
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureSyncfusionCore()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
