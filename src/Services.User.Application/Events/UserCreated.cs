using System;
using Convey.CQRS.Events;
using Services.User.Core.Entities;

namespace Services.User.Application.Events
{
    [Contract]
    public class UserCreated : IEvent
    {
        public Guid UserId { get; }
        public State State { get; }

        public UserCreated(Guid userId, State state)
        {
            UserId = userId;
            State = state;
        }
    }
}