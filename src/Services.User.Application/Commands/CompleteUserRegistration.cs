using System;
using Convey.CQRS.Commands;

namespace Services.User.Application.Commands
{
    [Contract]
    public class CompleteUserRegistration : ICommand
    {
        public Guid UserId { get; }
        public string FullName { get; }
        public string Address { get; }

        public CompleteUserRegistration(Guid userId, string fullName, string address)
        {
            UserId = userId;
            FullName = fullName;
            Address = address;
        }
    }
}