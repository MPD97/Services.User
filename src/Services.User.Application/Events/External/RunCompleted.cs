using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Services.User.Application.Events.External
{
    [Message("runs")]
    public class RunCompleted : IEvent
    {
        public Guid RunId { get; }
        public Guid CustomerId { get; }

        public RunCompleted(Guid runId, Guid customerId)
        {
            RunId = runId;
            CustomerId = customerId;
        }
    }
}