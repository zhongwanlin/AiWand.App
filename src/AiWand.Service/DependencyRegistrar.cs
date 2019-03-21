using AiWand.Core.Infrastructure;
using AiWand.Service.Users;
using Autofac;


namespace AiWand.Shop.Service
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
