using System.Threading.Tasks;

namespace CleanArchMVC.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(string email, string password);
        Task LogoutAsync();
    }
}
