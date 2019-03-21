using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AiWand.Core.Infrastructure;
using AiWand.Data;

namespace Api.Framework.Infrastructure
{
    public class AiWandStartup : IAiWandStartup
    {
        public int Order { get { return 1000; } }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().AddControllersAsServices();
            services.AddDbContext<AiWandDataContext>(options => options.UseMySql(configuration.GetConnectionString("AiWand")));
        }
    }
}
