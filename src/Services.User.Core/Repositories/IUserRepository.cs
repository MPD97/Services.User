﻿using System;
using System.Threading.Tasks;

namespace Services.User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User> GetAsync(Guid id);
        Task AddAsync(Entities.User user);
        Task UpdateAsync(Entities.User user);
    }
}