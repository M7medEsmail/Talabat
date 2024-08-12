using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedIdentityUser(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) // if Not fount User => Any return false -- !false = True
            {
                var user = new AppUser()
                {
                    FirstName = "Mohamed",
                    LastName = "Esmail",
                    Email = "m7med.esmail22@gmail.com",
                    UserName = "som3a",
                    PhoneNumber = "+201062838535",
                    Address = new Address()
                    {
                        City = "menouf",
                        Country = "Egypt",
                        Street = "GamalAbdelNasser"
                    }
                };
                await userManager.CreateAsync(user , "Pa$$w0rd");

            }
        }
    }
}
