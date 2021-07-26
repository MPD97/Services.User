using System;
using Convey.CQRS.Commands;

namespace Services.User.Application.Commands
{
    [Contract]
    public class CompleteUserRegistration : ICommand
    {
        public Guid UserId { get; }
        public string Pseudonym { get; }

        public CompleteUserRegistration(Guid userId, string pseudonym)
        {
            UserId = userId;
            Pseudonym = pseudonym;
        }
    }
}