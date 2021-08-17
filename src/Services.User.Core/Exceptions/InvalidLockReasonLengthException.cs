using System;

namespace Services.User.Core.Exceptions
{
    public class InvalidLockReasonLengthException : DomainException
    {
        public override string Code { get; } = "invalid_lock_reason_length";
        public Guid UserId { get; }
        public int GivenLength { get; }
        public int MinLength { get; }
        public int MaxLength { get; }

        public InvalidLockReasonLengthException(Guid userId, int givenLength, int minLength, int maxLength) 
            : base($"User with id: {userId} cannot be locked with reason length: {givenLength}. " +
                   $"Lock reason must be between {minLength} and {maxLength} characters long.")
        {
            UserId = userId;
            GivenLength = givenLength;
            MinLength = minLength;
            MaxLength = maxLength;
        }
    }
}