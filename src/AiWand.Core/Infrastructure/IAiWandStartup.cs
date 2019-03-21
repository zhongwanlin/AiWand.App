using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Infrastructure
{
    public interface IAiWandStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        void Configure(IApplicationBuilder app);

        int Order { get; }
    }
}
