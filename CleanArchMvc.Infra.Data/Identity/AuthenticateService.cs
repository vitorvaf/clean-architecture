using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AspNetUserManager<ApplicationUser> _userManager;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager,
            AspNetUserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email,
             password, false, lockoutOnFailure: false);
            
            return result.Succeeded;

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();            
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);

            if(result.Succeeded)
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);

            return result.Succeeded;
        }
    }
}