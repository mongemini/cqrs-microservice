using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mongemini.Bus.Contracts.Bus;
using Mongemini.Bus.RabbitMq.Options;

namespace Mongemini.Bus.RabbitMq
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOption>(configuration.GetSection(RabbitMqOption.RabbitMq).Bind);

            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var option = sp.GetService<IOptions<RabbitMqOption>> ();
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(option, sp.GetService<IMediator>(), scopeFactory);
            });
        }
    }
}
