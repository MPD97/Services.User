using System;

namespace Services.User.Application.Exceptions
{
    public class UserAlreadyRegisteredException : AppException
    {
        public override string Code { get; } = "user_already_registered";
        public Guid Id { get; }
        
        public UserAlreadyRegisteredException(Guid id) 
            : base($"User with id: {id} has already been registered.")
        {
            Id = id;
        }
    }
}