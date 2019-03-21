using AiWand.Core.Data;
using AiWand.Core.Domain;
using AiWand.Service.Users;
using Moq;
using Xunit;

namespace AiWand.Test.ServicesTest
{

    public class UserTest
    {
        private Mock<IUserService> _userModk;
        private Mock<IRepository<User>> _userRepositoryModk;

        public UserTest()
        {
            _userModk = new Mock<IUserService>();
            _userRepositoryModk = new Mock<IRepository<User>>();
        }

        [Fact]
        public void AddUserTest()
        {
            User user = null;
            _userModk.Setup(service => service.GetUser("")).Returns(user);
        }
    }
}
