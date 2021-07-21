using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
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
        
        public CompleteUserRegistrationHandler(IUserRepository userRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CompleteUserRegistration command)
        {
            var user = await _userRepository.GetAsync(command.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(command.UserId);
            }
            
            if (user.State is State.Valid)
            {
                throw new UserAlreadyRegisteredException(command.UserId);
            }

            user.CompleteRegistration(command.FullName, command.Address);
            await _userRepository.UpdateAsync(user);

            var events = _eventMapper.MapAll(user.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}