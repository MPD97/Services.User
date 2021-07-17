using System;

namespace Services.User.Core.Exceptions
{
    public class InvalidUserAddressException : DomainException
    {
        public override string Code { get; } = "invalid_user_address";
        public Guid Id { get; }
        public string Address { get; }

        public InvalidUserAddressException(Guid id, string address) : base(
            $"User with id: {id} has invalid address.")
        {
            Id = id;
            Address = address;
        }
    }
}