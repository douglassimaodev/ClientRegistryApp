using ClientRegistryApp.PageModels;
using ClientRegistryApp.Pages;
using ClientRegistryApp.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace ClientRegistryApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IClientService, ClientService>();

            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientsPageModel>();

            builder.Services.AddTransientWithShellRoute<ClientPage, ClientPageModel>(nameof(ClientPageModel));
            builder.Services.AddTransientWithShellRoute<ClientsPage, ClientsPageModel>(nameof(ClientsPageModel));

            return builder.Build();
        }
    }
}
