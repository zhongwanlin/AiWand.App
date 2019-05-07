using System;
using System.Collections.Generic;
using System.Text;
using AiWand.Core.Domain;
using AiWand.Core.DTO.Users;

namespace AiWand.Service.Login
{
    public interface ILoginService
    {
        User Login(LoginInput loginUser);

        User Regster(User registerUser);

        bool HasLogin(string token);
    }
}
