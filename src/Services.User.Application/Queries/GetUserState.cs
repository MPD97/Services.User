using System;
using Convey.CQRS.Queries;
using Services.User.Application.DTO;

namespace Services.User.Application.Queries
{
    public class GetUserState : IQuery<UserStateDto>
    {
        public Guid UserId { get; set; }
    }
}