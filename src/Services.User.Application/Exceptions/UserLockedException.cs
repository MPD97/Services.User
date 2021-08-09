using System;

namespace Services.User.Application.Exceptions
{
    public class UserLockedException : AppException
    {
        public override string Code { get; } = "user_locked";
        public Guid UserId { get; }
        
        public UserLockedException(Guid userId) 
            : base($"User with id: {userId} is locked.")
        {
            UserId = userId;
        }
    }
}