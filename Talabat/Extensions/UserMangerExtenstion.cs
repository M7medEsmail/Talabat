using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Domain.Entities.Identity;

namespace Talabat.Extensions
{
    public static class UserMangerExtenstion
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x =>x.Email ==email);
            return user;

        } 
    }
}
