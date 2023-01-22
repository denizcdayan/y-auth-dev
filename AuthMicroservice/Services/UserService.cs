using MinimalJwt.Models;
using MinimalJwt.Repositories;
using Novell.Directory.Ldap​;

namespace MinimalJwt.Services
{
    public class UserService : IUserService
    {
        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(userLogin.Username, 
                StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

            return user;
        }

        public List<User> GetUsers()
        {
            return UserRepository.Users;
        }

        public User Create(UserLogin userLogin)
        {
            // userlogin --> username + password
            User u = new();
            u.Username = userLogin.Username;
            u.EmailAddress = "dummyemail@mail.com"; // TODO: change
            u.Password = userLogin.Password;
            u.GivenName = "dummyGivenName"; // TODO: change
            u.Surname = "dummySurname"; // TODO: change
            u.Role = "Standard"; // TODO: change
            UserRepository.Users.Add(u);
            return u;
        }

        public bool ValidateUser(UserLogin userLogin)
        {
            // "ldap.forumsys.com:389/cn=read-only-admin,dc=example,dc=com"
            string domainName = "ldap.forumsys.com";
            string username = userLogin.Username;
            string password = userLogin.Password;

            //string userDn = "cn=read-only-admin,dc=example,dc=com"; //$"{username}@{domainName}";
            string userDn = "uid=" + username + ", dc=example,dc=com";

            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(domainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, password);
                    if (connection.Bound)
                    {
                        Console.WriteLine(connection.Connected);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;

            //return true;
        }
    }
}
