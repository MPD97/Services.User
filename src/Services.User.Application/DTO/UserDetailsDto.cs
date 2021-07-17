using System;
using System.Collections.Generic;

namespace Services.User.Application.DTO
{
    public class UserDetailsDto : UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public IEnumerable<Guid> CompletedRuns { get; set; }
    }
}