using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using AiWand.Core.Data;
using AiWand.Core.Domain;

namespace AiWand.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;


        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            if (user == null)
            {

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
