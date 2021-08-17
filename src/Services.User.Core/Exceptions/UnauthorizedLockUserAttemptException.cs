using System;

namespace Services.User.Core.Exceptions
{
    public class UnauthorizedLockUserAttemptException : DomainException
    {
        public override string Code { get; } = "unauthorized_lock_user_attempt";
        public Guid UserId { get; }
        public Guid UserToLockId { get; }

        public UnauthorizedLockUserAttemptException(Guid userId, Guid userToLockId) : base(
            $"User with id: {userId} attempted to lock user with id: {userToLockId} but was not authorized.")
        {
            UserId = userId;
            UserToLockId = userToLockId;
        }
    }
}