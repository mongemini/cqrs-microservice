using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mongemini.Application.Core.Behaviors;
using Mongemini.Bus.Contracts.Bus;
using Mongemini.Service.Application.EventHandlers;
using Mongemini.Service.Application.Events;
using Mongemini.Service.Application.Validators.Blanks;
using System.Reflection;

namespace Mongemini.Service.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BlankValidator>());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddTransient<IEventHandler<BlankEvent>, BlankEventHandler>();
        }

        public static void UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BlankEvent, BlankEventHandler>();
        }
    }
}
