using Notification_System_CRUD.Models;
using Notification_System_CRUD.Interfaces;
using System.Reflection.Metadata;

namespace Notification_System_CRUD.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetAll();   
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}