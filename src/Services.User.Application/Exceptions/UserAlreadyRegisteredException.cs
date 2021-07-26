using System;

namespace Services.User.Application.Exceptions
{
    public class UserAlreadyRegisteredException : AppException
    {
        public override string Code { get; } = "user_already_registered";
        public Guid UserId { get; }
        
        public UserAlreadyRegisteredException(Guid userId) 
            : base($"User with id: {userId} has already been registered.")
        {
            UserId = userId;
        }
    }
}