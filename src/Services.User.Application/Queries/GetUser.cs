using System;
using Convey.CQRS.Queries;
using Services.User.Application.DTO;

namespace Services.User.Application.Queries
{
    public class GetUser : IQuery<UserDetailsDto>
    {
        public Guid UserId { get; set; }
    }
}