using AppControle.DeskMob.Auth;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Shared.Repositories;

namespace AppControle.DeskMob
{
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7175/") }); //7175 //44380
            builder.Services.AddScoped<IRepository, Repository>();
            //Modal
            //builder.Services.AddBlazoredModal();
            //Auth
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationProviderJWT>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddSweetAlert2();
            //builder.Services.AddBlazoredModal();


            return builder.Build();
        }
    }
}
