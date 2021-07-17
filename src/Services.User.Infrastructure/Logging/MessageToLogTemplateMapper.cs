using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using Services.User.Application.Commands;
using Services.User.Application.Events.External;

namespace Services.User.Infrastructure.Logging
{
    public class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(CompleteUserRegistration),
                    new HandlerLogTemplate {After = "Completed a registration for the user with id: {UserId}."}
                },
                {
                    typeof(ChangeUserState),
                    new HandlerLogTemplate {After = "Changed a user with id: {UserId} state to: {State}."}
                },
                {
                    typeof(RunCompleted), new HandlerLogTemplate
                    {
                        After = "Run with id: {RunId} for the user with id: {UserId} has been completed."
                    }
                },
                {
                    typeof(SignedUp), new HandlerLogTemplate
                    {
                        After = "Created a new user with id: {UserId}."
                    }
                }
            };

        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}