using System;
using Services.User.Core.Entities;

namespace Services.User.Core.Exceptions
{
    public class CannotChangeUserStateException : DomainException
    {
        public override string Code { get; } = "invalid_user_state";
        public Guid UserId { get; }
        public State State { get; }

        public CannotChangeUserStateException(Guid userId, State state) : base(
            $"Cannot change user: {userId} state to: {state}.")
        {
            UserId = userId;
            State = state;
        }
    }
}