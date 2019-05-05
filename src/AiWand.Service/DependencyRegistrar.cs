using AiWand.Core.Infrastructure;
using AiWand.Service.CodeDocument;
using AiWand.Service.Login;
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
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerLifetimeScope();
            builder.RegisterType<CodeDocumentService>().As<ICodeDocumentService>().InstancePerLifetimeScope();
        }
    }
}
