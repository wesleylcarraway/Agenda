using Agenda.Infrastructure.Storage;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.UnitOfWork;
using Agenda.Infrastructure.Repositories;
using Agenda.ConsoleUI.Navigation;
using Agenda.Application.Services;
using Agenda.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Agenda.ConsoleUI.Views;
using Agenda.ConsoleUI.Utils;
using Agenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agenda.ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(services);
                }).ConfigureLogging((context, logging) => {
                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection("Logging");
                logging.AddConfiguration(config);
                logging.AddConsole();
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                logging.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);

            });

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=AgendaLuby; Integrated Security=true;");
                },

                ServiceLifetime.Singleton
            );

            services.AddSingleton<IJsonStorage<Interaction>, JsonStorage<Interaction>>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IInteractionRepository, InteractionRepository>();
            services.AddTransient<IAgendaService, AgendaService>();
            services.AddTransient<IInteractionService, InteractionService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<ConsoleMain>();
            services.AddTransient<CreateContact>();
            services.AddTransient<EditContact>();
            services.AddTransient<ConsoleInput>();
            services.AddTransient<HomeView>();
            services.AddTransient<ListSchedule>();
            services.AddTransient<ConsoleMain>();
            services.AddSingleton<ConsoleNavigation>();

            services.AddHostedService<ConsoleHostedService>();
        }
    }
}
