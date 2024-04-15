
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Model.Constants;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaulData(IServiceProvider serviceProvider)
        {
            var userMgr = serviceProvider.GetService<UserManager<User>>();
            var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //adding role
            if (!await roleMgr.RoleExistsAsync(Roles.User))
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.User));
            }

            if (!await roleMgr.RoleExistsAsync(Roles.Admin))
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin));
            }
            var admin = new User
            {
                UserName = "admin@gmail.com",
                FirstName="Admin",
                LastName="Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            var isAdminExist = await userMgr.FindByEmailAsync(admin.Email);
            if (isAdminExist is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin);
            }
            else
            {

            }
        }
    }
}
