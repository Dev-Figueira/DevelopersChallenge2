using Microsoft.Extensions.DependencyInjection;
using Nibo.Business.Interfaces;
using Nibo.Business.Service;
using Nibo.Data.Context;
using Nibo.Data.Repository;
using Nibo.Business.Notifications;

namespace Nibo.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<INotification, Notifier>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
