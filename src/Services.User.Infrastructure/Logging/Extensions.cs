using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using Services.User.Application.Commands;

namespace Services.User.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(CompleteUserRegistration).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}