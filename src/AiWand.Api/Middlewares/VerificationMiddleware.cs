using AiWand.Service.Login;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiWand.Api.Middlewares
{
    public class VerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoginService _loginService;

        public VerificationMiddleware(RequestDelegate next, ILoginService loginService)
        {
            _next = next;
            _loginService = loginService;
        }

        public async Task Invoke(HttpContext context)
        {
            await Task.Run(() =>
            {
                var token = context.Request.Headers["token"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception("请登录");
                }
                if (_loginService.HasLogin(token)==false)
                {
                    throw new Exception("请登录");
                }

            });
            await _next.Invoke(context);
        }
    }
}
