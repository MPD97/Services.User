using System;
using Convey.CQRS.Events;

namespace Services.User.Application.Events
{
    [Contract]
    public class UserStateChanged : IEvent
    {
        public Guid UserId { get; }
        public string CurrentState { get; }
        public string PreviousState { get; }

        public UserStateChanged(Guid userId, string currentState, string previousState)
        {
            UserId = userId;
            CurrentState = currentState;
            PreviousState = previousState;
        }
    }
}