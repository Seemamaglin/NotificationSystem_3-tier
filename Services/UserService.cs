using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using System.Reflection.Metadata;

namespace NotificationSystem_3_tier.Services
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