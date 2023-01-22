

namespace MinimalJwt.Services
{
    public interface IAuthenticationService
    {
        MinimalJwt.Models.User Login(string username, string password);
    }
}
 