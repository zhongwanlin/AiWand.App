using System;
using System.Collections.Generic;
using System.Text;
using AiWand.Core.Domain;
using AiWand.Core.DTO.Users;
using AiWand.Service.Users;

namespace AiWand.Service.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;

        public LoginService(IUserService userService)
        {
            _userService = userService;
        }

        public User Login(LoginInput loginUser)
        {
            //获取用户信息
            var user = _userService.GetUser(loginUser.UserName, loginUser.Password);

            //更新用户登录信息
            _userService.Update(user);

            return user;
        }

        public User Regster(User registerUser)
        {
            throw new NotImplementedException();
        }
    }
}
