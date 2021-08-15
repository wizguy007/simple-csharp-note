using System;
namespace simple_note.User
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity saveUser(UserEntity user)
        {
            return _userRepository.Create(user);
        }

        public UserEntity FindById(int id)
        {
            return _userRepository.FindById(id);
        }

        public UserEntity FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }
    }
}
