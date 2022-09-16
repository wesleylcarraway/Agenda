using Agenda.MVC.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Agenda.MVC.Config
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews(opts =>
            {
                opts.Filters.Add(new ApplicationExceptionFilter());
            });

            services
                .AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                })
                .AddAuthorization()
                .AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opts.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opts.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(opts =>
                {
                    opts.LoginPath = "/Login";
                    opts.LogoutPath = "/Logout";
                    opts.AccessDeniedPath = "/Home";
                });
            return services;
        }
    }
}
