using System;
using Convey.CQRS.Events;

namespace Services.User.Application.Events
{
    [Contract]
    public class UserCreated : IEvent
    {
        public Guid UserId { get; }
        
        public UserCreated(Guid userId)
        {
            UserId = userId;
        }
    }
}