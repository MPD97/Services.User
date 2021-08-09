using System;

namespace Services.User.Application.Exceptions
{
    public class InvalidLockUserReasonException: AppException
    {
        public override string Code { get; } = "invalid_lock_user_reason";
        public Guid UserId { get; }
        public string Reason { get; }

        public InvalidLockUserReasonException(Guid userId, string reason)
            : base($"User with id: {userId} cannot be locked with reason: {reason}.")
        {
            UserId = userId;
            Reason = reason;
        }
    }
}