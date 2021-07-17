using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using Services.User.Application.Services;
using Services.User.Core;
using Services.User.Core.Events;

namespace Services.User.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case UserRegistrationCompleted e: return new Application.Events.UserCreated(e.User.Id);
                case UserStateChanged e:
                    return new Application.Events.UserStateChanged(e.User.Id,
                        e.User.State.ToString().ToLowerInvariant(), e.PreviousState.ToString().ToLowerInvariant());
            }

            return null;
        }
    }
}