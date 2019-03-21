using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiWand.Core.Caching.Redis;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiWand.Core.Infrastructure
{
    public class AiWandEngine : IEngine
    {
        private IServiceProvider _serviceProvider;

        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            var typeFinder = new WebAppTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<IAiWandStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IAiWandStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure request pipeline
            foreach (var instance in instances)
                instance.Configure(application);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var typeFinder = new WebAppTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<IAiWandStartup>();

            var instances = startupConfigurations
                .Select(startup => (IAiWandStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure services
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            RegisterDependencies(services, typeFinder);
            return _serviceProvider;
        }

        public void Initialize(IServiceCollection services)
        {
            //初始化
        }



        /// <summary>
        /// Register dependencies using Autofac
        /// </summary>
        /// <param name="nopConfig">Startup Nop configuration parameters</param>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        protected virtual IServiceProvider RegisterDependencies(IServiceCollection services, ITypeFinder typeFinder)
        {
            var containerBuilder = new ContainerBuilder();
            //register engine
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
            //register type finder
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
            {
                dependencyRegistrar.Register(containerBuilder, typeFinder);
            }

            //containerBuilder.RegisterType<RedisDb>().As<RedisDb>().InstancePerLifetimeScope();

            //populate Autofac container builder with the set of registered service descriptors
            containerBuilder.Populate(services);

            //create service provider
            _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
            return _serviceProvider;
        }
    }
}
