using MinimalJwt.Models;

namespace MinimalJwt.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
        public List<User> GetUsers();
        public bool ValidateUser(UserLogin userLogin);
        public User Create(UserLogin userLogin);
    }
}
