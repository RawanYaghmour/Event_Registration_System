using Event_Registration_System.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Event_Registration_System.Repository.Interfaces
{
    public interface IAcount
    {
        public Task<UserDto> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState);

        public Task<UserDto> LoginUser(string Username, string Password);
    }
}
