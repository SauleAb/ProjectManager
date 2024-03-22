using Syncfusion.Maui.Core.Hosting;

namespace LandscapeProjectsManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
			builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Goldoni_DEMO.otf", "TitleFont");
                    fonts.AddFont("Pompiere-Regular.ttf", "AppFont");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}
