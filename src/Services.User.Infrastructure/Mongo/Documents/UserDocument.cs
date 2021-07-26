using System;
using System.Collections.Generic;
using Convey.Types;
using Services.User.Core.Entities;

namespace Services.User.Infrastructure.Mongo.Documents
{

    public class UserDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Pseudonym { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Guid> CompletedRuns { get; set; }
    }
}