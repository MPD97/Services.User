using System;
using Convey.CQRS.Commands;

namespace Services.User.Application.Commands
{
    [Contract]
    public class LockUser : ICommand
    {
        public Guid UserId { get; }
        public string Reason { get; }    
        
        public LockUser(Guid userId, string reason)
        {
            UserId = userId;
            Reason = reason;
        }
    }
}