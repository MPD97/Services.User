using System;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Services.User.Application.DTO;
using Services.User.Application.Queries;
using Services.User.Infrastructure.Mongo.Documents;

namespace Services.User.Infrastructure.Mongo.Queries.Handlers
{
    public class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
    {
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;
        private readonly IDistributedCache _cache;
        private static readonly DistributedCacheEntryOptions CacheOptions = new()
            {SlidingExpiration = TimeSpan.FromHours(1)};
       

        public GetUserHandler(IMongoRepository<UserDocument, Guid> userRepository, IDistributedCache cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<UserDetailsDto> HandleAsync(GetUser query)
        {
            var cached = await _cache.GetAsync(query.UserId.ToString());
            if (cached is {})
               return Utf8Json.JsonSerializer.Deserialize<UserDetailsDto>(cached);
            
            var document = await _userRepository.GetAsync(p => p.Id == query.UserId);
            var dto = document?.AsDetailsDto();
            
            await _cache.SetAsync(query.UserId.ToString(), Utf8Json.JsonSerializer.Serialize(dto), CacheOptions);

            return dto;
        }
    }
}