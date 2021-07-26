using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Services.User.Application.Services;
using Services.User.Core.Exceptions;
using Services.User.Core.Repositories;

namespace Services.User.Application.Events.External.Handlers
{
    public class RunCompletedHandler : IEventHandler<RunCompleted>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public RunCompletedHandler(IUserRepository userRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(RunCompleted @event)
        {
            var customer = await _userRepository.GetAsync(@event.UserId);
            if (customer is null)
            {
                throw new UserNotFoundException(@event.UserId);
            }

            customer.AddCompletedRun(@event.RunId);
            await _userRepository.UpdateAsync(customer);
            
            var events = _eventMapper.MapAll(customer.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}