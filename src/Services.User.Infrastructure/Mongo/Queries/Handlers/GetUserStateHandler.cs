using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.User.Application.DTO;
using Services.User.Application.Queries;
using Services.User.Infrastructure.Mongo.Documents;

namespace Services.User.Infrastructure.Mongo.Queries.Handlers
{
    public class GetUserStateHandler : IQueryHandler<GetUserState, UserStateDto>
    {
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;

        public GetUserStateHandler(IMongoRepository<UserDocument, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserStateDto> HandleAsync(GetUserState query)
        {
            var document = await _userRepository.GetAsync(p => p.Id == query.UserId);

            return document is null
                ? null
                : new UserStateDto
                {
                    Id = document.Id,
                    State = document.State.ToString().ToLowerInvariant()
                };
        }
    }
}