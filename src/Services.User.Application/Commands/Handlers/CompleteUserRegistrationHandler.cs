using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Caching.Distributed;
using Services.User.Application.Exceptions;
using Services.User.Application.Services;
using Services.User.Core.Entities;
using Services.User.Core.Exceptions;
using Services.User.Core.Repositories;

namespace Services.User.Application.Commands.Handlers
{
    public class CompleteUserRegistrationHandler : ICommandHandler<CompleteUserRegistration>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private readonly IDistributedCache _cache;

        public CompleteUserRegistrationHandler(IUserRepository userRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker, IDistributedCache cache)
        {
            _userRepository = userRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _cache = cache;
        }

        public async Task HandleAsync(CompleteUserRegistration command)
        {
            var user = await _userRepository.GetAsync(command.UserId);
            if (user is null)
                throw new UserNotFoundException(command.UserId);
            
            if (user.State is State.Valid)
                throw new UserAlreadyRegisteredException(command.UserId);

            if (await _userRepository.ExistsAsync(command.Pseudonym))
                throw new UserAlreadyRegisteredException(command.UserId);
            
            user.CompleteRegistration(command.Pseudonym);
            await _userRepository.UpdateAsync(user);

            var events = _eventMapper.MapAll(user.Events);
            await _messageBroker.PublishAsync(events.ToArray());

            await _cache.RemoveAsync(user.Id.ToString());
        }
    }
}