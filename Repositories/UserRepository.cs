using Notification_System_CRUD.Models;
using Notification_System_CRUD.Interfaces;
using System.Net.Http.Headers;

namespace Notification_System_CRUD.Repositories
{
    public class UserRepository: IUserRepository
    {
        private List<User> users= new();
        public void Add(User user)
        {
            users.Add(user);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User? GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);

        }

        public void Update(User user)
        {
            var existingUser=GetById(user.Id);
            if (existingUser==null)
            {
                Console.WriteLine("User not found!");
                return;
            }
            existingUser.Name=user.Name;
            existingUser.Email=user.Email;
            existingUser.PhoneNumber=user.PhoneNumber;
        }

        public void Delete(int id)
        {
            var user=GetById(id);
            if (user==null)
            {
                Console.WriteLine("User not found!");
                return;
            }
            users.Remove(user);
        }
    }
}