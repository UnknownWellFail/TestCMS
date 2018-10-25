using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TestCMS.Models;

namespace TestCMS.Services
{
    public class UserService
    {
        
        private readonly UserRepository userRepository;
        private IMemoryCache cache;

        public UserService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            userRepository = new UserRepository(configuration);
            cache = memoryCache;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users = null;
            if (!cache.TryGetValue("allUsers", out users))
            {
                users = await Task.FromResult(userRepository.GetAll());
                if (users != null)
                {
                    cache.Set("allUsers", users,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return users;
        }
        
        
        public void updateUser(User user)
        {
            userRepository.Update(user);
        }

        public void removeUser(User user)
        {
            userRepository.Delete(user.id);
        }

        public void addUser(User user)
        {
            userRepository.Create(user);
        }

    }
}