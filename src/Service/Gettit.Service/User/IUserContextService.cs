using Gettit.Data.Models;

namespace Gettit.Service.User
{
    public interface IUserContextService
    {
        Task<GettitUser> GetCurrentUserAsync();
    }
}
