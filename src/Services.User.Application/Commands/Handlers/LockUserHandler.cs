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
    public class LockUserHandler : ICommandHandler<LockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IAppContext _appContext;
        private readonly IDistributedCache _cache;

        public LockUserHandler(IUserRepository userRepository, IEventMapper eventMapper, IMessageBroker messageBroker,
            IDateTimeProvider dateTimeProvider, IAppContext appContext, IDistributedCache cache)
        {
            _userRepository = userRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _dateTimeProvider = dateTimeProvider;
            _appContext = appContext;
            _cache = cache;
        }

        public async Task HandleAsync(LockUser command)
        {
            if (string.IsNullOrWhiteSpace(command.Reason))
                throw new InvalidLockUserReasonException(command.UserId, command.Reason);
            
            var identity = _appContext.Identity;
            if (!identity.IsAuthenticated || !identity.IsAdmin)
                throw new UnauthorizedLockUserAttemptException(identity.Id, command.UserId);
            
            var user = await _userRepository.GetAsync(command.UserId);
            if (user is null)
                throw new UserNotFoundException(command.UserId);
            
            if (user.State == State.Locked)
                return;
            
            user.Lock(identity.Id, command.Reason, _dateTimeProvider.Now);
            
            await _userRepository.UpdateAsync(user);
            var events = _eventMapper.MapAll(user.Events);
            await _messageBroker.PublishAsync(events.ToArray());
            
            await _cache.RemoveAsync(user.Id.ToString());
        }
    }
}