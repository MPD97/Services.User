using System;
using Services.User.Core.Entities;

namespace Services.User.Core.Exceptions
{
    public class CannotChangeUserStateException : DomainException
    {
        public override string Code { get; } = "invalid_user_state";
        public Guid Id { get; }
        public State State { get; }

        public CannotChangeUserStateException(Guid id, State state) : base(
            $"Cannot change user: {id} state to: {state}.")
        {
            Id = id;
            State = state;
        }
    }
}