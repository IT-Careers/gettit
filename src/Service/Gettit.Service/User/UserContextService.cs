using Gettit.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gettit.Service.User
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserStore<GettitUser> userStore;

        public UserContextService(
            IHttpContextAccessor httpContextAccessor, 
            IUserStore<GettitUser> userStore)
        {
            _httpContextAccessor = httpContextAccessor;
            this.userStore = userStore;
        }

        public async Task<GettitUser> GetCurrentUserAsync()
        {
            string? userId = this._httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await this.userStore.FindByIdAsync(userId, CancellationToken.None);
        }
    }
}
