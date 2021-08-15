using System;
using System.Linq;

namespace simple_note.User
{
    public class UserRepository : IUserRepository
    {
        private readonly SimpleTodoEntities _entities;

        public UserRepository(SimpleTodoEntities entities)
        {
            _entities = entities;
        }

        public UserEntity Create(UserEntity user)
        {
            _entities.Users.Add(user);

            user.Id = _entities.SaveChanges();

            return user;
        }

        public UserEntity FindByEmail(string email)
        {
            return _entities.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public UserEntity FindById(int id)
        {
            return _entities.Users.Where(u => u.Id == id).FirstOrDefault();
        }
    }

    public interface IUserRepository
    {
        public UserEntity Create(UserEntity user);
        public UserEntity FindByEmail(string email);
        public UserEntity FindById(int id);
    }
}
