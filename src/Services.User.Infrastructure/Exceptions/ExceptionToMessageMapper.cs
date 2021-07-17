using System;
using Convey.MessageBrokers.RabbitMQ;
using Services.User.Application.Commands;
using Services.User.Application.Events.Rejected;
using Services.User.Application.Exceptions;
using Services.User.Core.Exceptions;

namespace Services.User.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotChangeUserStateException ex => message switch
                {
                    ChangeUserState _ => new ChangeUserStateRejected(ex.Id,
                        ex.State.ToString().ToLowerInvariant(), ex.Message, ex.Code),
                    CompleteUserRegistration _ => new CompleteUserRegistrationRejected(ex.Id, ex.Message,
                        ex.Code),
                    _ => null
                },
                UserAlreadyRegisteredException ex => new CompleteUserRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                UserNotFoundException ex => new CompleteUserRegistrationRejected(ex.Id, ex.Message, ex.Code),
                InvalidUserFullNameException ex => new CompleteUserRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                InvalidUserAddressException ex => new CompleteUserRegistrationRejected(ex.Id, ex.Message,
                    ex.Code),
                _ => null
            };
    }
}