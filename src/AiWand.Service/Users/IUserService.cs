using AiWand.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Service.Users
{
    public interface IUserService
    {
        User GetUser(string id);

        User GetUser(string userName, string password);

        void Add(User user);

        void Update(User user);

        bool ExistUserName(string userName);
    }
}
