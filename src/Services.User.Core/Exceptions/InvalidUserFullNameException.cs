using System;

namespace Services.User.Core.Exceptions
{
    public class InvalidUserFullNameException : DomainException
    {
        public override string Code { get; } = "invalid_user_fullname";
        public Guid Id { get; }
        public string FullName { get; }

        public InvalidUserFullNameException(Guid id, string fullName) : base(
            $"User with id: {id} has invalid full name.")
        {
            Id = id;
            FullName = fullName;
        }
    }
}