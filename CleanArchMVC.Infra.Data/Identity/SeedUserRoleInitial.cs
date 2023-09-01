using CleanArchMVC.Domain.Account;
using CleanArchMVC.Domain.Account.Roles;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchMVC.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            SeedRole(AccountRole.User);
            SeedRole(AccountRole.Admin);
        }

        public void SeedUsers()
        {
            SeedUser("usuario@localhost", "Senh@123", AccountRole.User);
            SeedUser("admin@localhost", "Senh@123", AccountRole.Admin);
        }

        private void SeedUser(string email, string password, AccountRole accountRole)
        {
            string roleName = accountRole.ToString();

            if (_userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                IdentityResult result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, roleName).Wait();
                }
            }
        }

        private void SeedRole(AccountRole accountRole)
        {
            string roleName = accountRole.ToString();

            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                };

                _roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
