using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using AiWand.Core.Data;
using AiWand.Core.Domain;
using AiWand.Core.DTO.Users;

namespace AiWand.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;


        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Register(UserRegister userRegister)
        {
            if (userRegister == null)
            {
                throw new Exception("注册信息为空");
            }
            if (string.IsNullOrWhiteSpace(userRegister.UserName))
            {
                throw new Exception("用户名为空");
            }
            if (string.IsNullOrWhiteSpace(userRegister.Password))
            {
                throw new Exception("密码为空");
            }
            if (ExistUserName(userRegister.UserName))
            {
                throw new Exception($"用户名{userRegister.UserName}已存在");
            }
            User user = new User
            {
                UserName = userRegister.UserName,
                Password = userRegister.Password,
                RegisterTime = DateTime.Now,
                Creator = "sys"
            };

            Add(user);

            return GetUser(userRegister.UserName, userRegister.Password);
        }

        public void Add(User user)
        {
            if (user == null)
            {
                throw new Exception("用户信息为空");
            }
            user.CreateTime = DateTime.Now;
            user.RegisterTime = user.CreateTime;
            user.Status = Core.Enums.EStatus.正常;
            user.UserType = Core.Enums.EUserType.普通用户;
            _userRepository.Insert(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public User GetUser(string id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserByToken(string token)
        {
            return _userRepository.Table.Where(u => u.Token == token).FirstOrDefault();
        }

        public bool ExistUserName(string userName)
        {
            return _userRepository.Table.Count(u => u.UserName == userName) > 0;
        }

        public User GetUser(string userName, string password)
        {
            var query = from u in _userRepository.Table
                        where u.UserName == userName && u.Password == password
                        select u;

            return query.FirstOrDefault();
        }
    }
}
