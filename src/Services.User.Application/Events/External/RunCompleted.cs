using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Services.User.Application.Events.External
{
    [Message("runs")]
    public class RunCompleted : IEvent
    {
        public Guid RunId { get; }
        public Guid UserId { get; }

        public RunCompleted(Guid runId, Guid userId)
        {
            RunId = runId;
            UserId = userId;
        }
    }
}