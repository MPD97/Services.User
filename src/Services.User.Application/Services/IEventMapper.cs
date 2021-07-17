using System.Collections.Generic;
using Convey.CQRS.Events;
using Services.User.Core;

namespace Services.User.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}