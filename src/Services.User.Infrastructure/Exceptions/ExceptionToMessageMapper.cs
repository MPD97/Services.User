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
                    ChangeUserState _ => new ChangeUserStateRejected(ex.UserId,
                        ex.State.ToString().ToLowerInvariant(), ex.Message, ex.Code),
                    LockUser _ => new ChangeUserStateRejected(ex.UserId,
                        ex.State.ToString().ToLowerInvariant(), ex.Message, ex.Code),
                    CompleteUserRegistration _ => new CompleteUserRegistrationRejected(ex.UserId, ex.Message,
                        ex.Code),
                    _ => null
                },
                UserAlreadyRegisteredException ex => new CompleteUserRegistrationRejected(ex.UserId, ex.Message,
                    ex.Code),
                UserNotFoundException ex => new CompleteUserRegistrationRejected(ex.UserId, ex.Message, ex.Code),
                InvalidUserPseudonymException ex => new CompleteUserRegistrationRejected(ex.UserId, ex.Message,
                    ex.Code),
                InvalidUserPseudonymLengthException  ex => new CompleteUserRegistrationRejected(ex.UserId, ex.Message,
                    ex.Code),
                _ => null
            };
    }
}