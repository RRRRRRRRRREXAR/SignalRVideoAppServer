using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;

namespace VideoAppBLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task Register(UserDTO User);
        Task<UserDTO> Login(string email, string password);
        Task ChangePassword(string email, string oldPassword, string newPassword);

    }
}
