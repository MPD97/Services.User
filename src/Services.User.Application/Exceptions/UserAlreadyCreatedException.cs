using System;

namespace Services.User.Application.Exceptions
{
    public class UserAlreadyCreatedException: AppException
    {
        public override string Code { get; } = "user_already_created";
        public Guid CustomerId { get; }

        public UserAlreadyCreatedException(Guid customerId)
            : base($"User with id: {customerId} was already created.")
        {
            CustomerId = customerId;
        }
    }
}