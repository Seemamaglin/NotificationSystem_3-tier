
using NotificationSystem_3_tier.Models;
using System.Collections.Generic;

namespace NotificationSystem_3_tier.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        List<User> GetAll();
        User? GetById(int id);
        void Update(User user);
        void Delete(int id);
    }
}
