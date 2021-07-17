using System;
using Convey.CQRS.Events;

namespace Services.User.Application.Events.Rejected
{
    [Contract]
    public class ChangeCustomerStateRejected : IRejectedEvent
    {
        public Guid UserId { get; }
        public string State { get; }
        public string Reason { get; }
        public string Code { get; }

        public ChangeCustomerStateRejected(Guid userId, string state, string reason, string code)
        {
            UserId = userId;
            State = state;
            Reason = reason;
            Code = code;
        }
    }
}