using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AiWand.Core.Infrastructure;
using AiWand.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace Api.Framework.Infrastructure
{
    public class AiWandStartup : IAiWandStartup
    {
        public int Order { get { return 1000; } }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().AddControllersAsServices();
            services.AddDbContext<AiWandDataContext>(options => options.UseMySql(configuration.GetConnectionString("AiWand")));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }
    }
}
