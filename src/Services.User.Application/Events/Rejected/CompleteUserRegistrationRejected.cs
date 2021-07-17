using System;
using Convey.CQRS.Events;

namespace Services.User.Application.Events.Rejected
{
    [Contract]
    public class CompleteUserRegistrationRejected : IRejectedEvent
    {
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CompleteUserRegistrationRejected(Guid userId, string reason, string code)
        {
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}