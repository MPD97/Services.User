using System;

namespace Services.User.Core.Exceptions
{
    public class InvalidUserPseudonymException : DomainException
    {
        public override string Code { get; } = "invalid_user_pseudonym";
        public Guid UserId { get; }
        public string Pseudonym { get; }

        public InvalidUserPseudonymException(Guid userId, string pseudonym) : base(
            $"User with id: {userId} has invalid pseudonym: {pseudonym}.")
        {
            UserId = userId;
            Pseudonym = pseudonym;
        }
    }
}