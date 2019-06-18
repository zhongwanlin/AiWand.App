using AiWand.Core.Domain;
using AiWand.Core.DTO.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Service.Users
{
    public interface IUserService
    {
        User GetUser(string id);

        User GetUserByToken(string token);

        User GetUser(string userName, string password);

        void Add(User user);

        void Update(User user);

        bool ExistUserName(string userName);

        User Register(UserRegister userRegister);
    }
}
