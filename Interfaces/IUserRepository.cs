
using Notification_System_CRUD.Models;
using System.Collections.Generic;

namespace Notification_System_CRUD.Interfaces
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
