using System;
using Services.User.Application.Services;

namespace Services.User.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {        
        public DateTime Now  => DateTime.UtcNow;
    }
}