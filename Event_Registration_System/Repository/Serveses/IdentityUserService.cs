using Event_Registration_System.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using Event_Registration_System.Repository.Interfaces;
using Event_Registration_System.Data;
using Event_Registration_System.Models;

namespace Event_Registration_System.Repository.Serveses
{
    public class IdentityUserService : IAcount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityUserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDto> Register(RegisterUserDTO userData, ModelStateDictionary modelState)
        {
            var user = new User()
            {
                UserName = userData.UserName,
                Email = userData.Email
            };

            var result = await _userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? nameof(userData.Password) :
                                error.Code.Contains("Email") ? nameof(userData.Email) :
                                error.Code.Contains("UserName") ? nameof(userData.UserName) : "";

                modelState.AddModelError(errorCode, error.Description);
            }

            return null;
        }



        public async Task<UserDto> LoginUser(string Username, string Password)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return null; 
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, Password, false, false);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            return null; 
        }

    }
}
