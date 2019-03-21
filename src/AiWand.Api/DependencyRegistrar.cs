using AiWand.Core.Data;
using AiWand.Core.Infrastructure;
using AiWand.Data;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiWand.Api
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;



        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(context => new AiWandDataContext(context.Resolve<DbContextOptions<AiWandDataContext>>())).As<IDbContext>().InstancePerLifetimeScope();
            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
