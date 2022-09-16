using System.Reflection;
using Agenda.Application.Interfaces;
using Agenda.Application.Services;
using Agenda.Application.Utils;
using Agenda.Application.Validations;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Data;
using Agenda.Infrastructure.Repositories;
using Agenda.Infrastructure.Storage;
using Agenda.Infrastructure.UnitOfWork;
using FluentValidation.AspNetCore;

namespace Agenda.API.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationContext>();

            services.AddSingleton<IJsonStorage<Interaction>, JsonStorage<Interaction>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IInteractionRepository, InteractionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<IAgendaAdminService, AgendaAdminService>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommonUserService, CommonUserService>();

            services.AddScoped<IInteractionService, InteractionService>();

            services.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>(
                    asr => !(asr.ValidatorType.GetCustomAttribute<IgnoreInjectionAttribute>()?.Ignore ?? false)
                );
            });

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
