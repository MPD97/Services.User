using System;
using Convey.CQRS.Commands;

namespace Services.User.Application.Commands
{
    [Contract]
    public class ChangeUserState : ICommand
    {
        public Guid UserId { get; }
        public string State { get; }

        public ChangeUserState(Guid userId, string state)
        {
            UserId = userId;
            State = state;
        }
    }
}