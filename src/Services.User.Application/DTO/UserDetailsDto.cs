using System;
using System.Collections.Generic;

namespace Services.User.Application.DTO
{
    public class UserDetailsDto : UserDto
    {
        public string Email { get; set; }
        public string Pseudonym { get; set; }
        public IEnumerable<Guid> CompletedRuns { get; set; }
    }
}