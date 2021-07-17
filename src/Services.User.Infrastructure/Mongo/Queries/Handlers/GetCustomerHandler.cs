using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.User.Application.DTO;
using Services.User.Application.Queries;
using Services.User.Infrastructure.Mongo.Documents;

namespace Services.User.Infrastructure.Mongo.Queries.Handlers
{
    public class GetCustomerHandler : IQueryHandler<GetUser, UserDetailsDto>
    {
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;

        public GetCustomerHandler(IMongoRepository<UserDocument, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDetailsDto> HandleAsync(GetUser query)
        {
            var document = await _userRepository.GetAsync(p => p.Id == query.UserId);

            return document?.AsDetailsDto();
        }
    }
}