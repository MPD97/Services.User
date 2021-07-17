using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.User.Application.DTO;
using Services.User.Application.Queries;
using Services.User.Infrastructure.Mongo.Documents;

namespace Services.User.Infrastructure.Mongo.Queries.Handlers
{
    public class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;

        public GetUsersHandler(IMongoRepository<UserDocument, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        {
            var customers = await _userRepository.FindAsync(_ => true);

            return customers.Select(p => Documents.Extensions.AsDto((UserDocument) p));
        }
    }
}