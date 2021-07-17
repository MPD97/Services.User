using System.Collections.Generic;
using Convey.CQRS.Queries;
using Services.User.Application.DTO;

namespace Services.User.Application.Queries
{
    public class GetUsers : IQuery<IEnumerable<UserDto>>
    {
    }
}