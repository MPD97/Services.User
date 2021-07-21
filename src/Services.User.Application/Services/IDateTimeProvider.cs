using System;

namespace Services.User.Application.Services
{
        public interface IDateTimeProvider
        {
            DateTime Now { get; }
        }
}