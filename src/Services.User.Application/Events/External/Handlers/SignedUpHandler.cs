using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Services.User.Application.Exceptions;
using Services.User.Application.Services;
using Services.User.Core.Repositories;

namespace Services.User.Application.Events.External.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private const string RequiredRole = "user";
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SignedUpHandler(IUserRepository userRepository, IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            if (@event.Role != RequiredRole)
            {
                throw new InvalidRoleException(@event.UserId, @event.Role, RequiredRole);
            }

            var user = await _userRepository.GetAsync(@event.UserId);
            if (user is {})
            {
                throw new UserAlreadyCreatedException(user.Id);
            }

            user = new Core.Entities.User(@event.UserId, @event.Email, _dateTimeProvider.Now);
            await _userRepository.AddAsync(user);
        }
    }
}