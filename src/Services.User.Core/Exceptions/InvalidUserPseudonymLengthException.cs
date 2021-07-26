using System;

namespace Services.User.Core.Exceptions
{
    public class InvalidUserPseudonymLengthException : DomainException
    {
        public override string Code { get; } = "invalid_user_pseudonym_length";
        public Guid UserId { get; }
        public int PseudonymLength { get; }
        public int MinLength { get; }
        public int MaxLength { get; }

        public InvalidUserPseudonymLengthException(Guid userId, int pseudonymLength, int minLength, int maxLength) 
            : base($"User with id: {userId} has invalid pseudonym length: {pseudonymLength}. " +
                   $"Pseudonym length should be between: {minLength} and {maxLength} characters long.")
        {
            UserId = userId;
            PseudonymLength = pseudonymLength;
            MinLength = minLength;
            MaxLength = maxLength;
        }
    }
}