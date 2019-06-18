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
            if (loginUser == null)
            {
                throw new Exception("用户名/密码为空");
            }
            if (string.IsNullOrWhiteSpace(loginUser.UserName))
            {
                throw new Exception("用户名为空");
            }
            if (string.IsNullOrWhiteSpace(loginUser.Password))
            {
                throw new Exception("用户名为空");
            }
            //获取用户信息
            var user = _userService.GetUser(loginUser.UserName, loginUser.Password);
            if (user == null)
            {
                throw new Exception("用户名或密码不正确");
            }
            user.Token = Guid.NewGuid().ToString("N");
            user.LastLoginTime = user.LoginTime;
            user.LoginTime = DateTime.Now;
            //更新用户登录信息
            _userService.Update(user);

            return user;
        }

        public User Regster(User registerUser)
        {
            throw new NotImplementedException();
        }

        public bool HasLogin(string token)
        {
            var user = _userService.GetUserByToken(token);

            return user != null;
        }
    }
}
