using AiWand.Core;
using AiWand.Service.Login;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AiWand.Api.ActionFilters
{
    public class LoginAttribute : ActionFilterAttribute
    {
        private readonly ILoginService _loginService;
        private readonly IHostingEnvironment _env;

        public LoginAttribute(ILoginService loginService, IHostingEnvironment env)
        {
            _loginService = loginService;
            _env = env;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_env.IsDevelopment())
                return;

            var attributes = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor)
            .MethodInfo.CustomAttributes.ToList();
            //如果是不需要授权的，直接跳过
            if (attributes.Count(it => it.AttributeType.Name.Contains("UnAuthAttribute")) > 0)
                return;

            Result result = new Result(true);
            //如果包含授权特性则进行，授权验证
            try
            {
                //登录验证
                var token = filterContext.HttpContext.Request.Headers["token"];
                if (string.IsNullOrWhiteSpace(token) || _loginService.HasLogin(token) == false)
                {
                    result.IsSuccessed = false;
                    result.Data = "请登录";

                    filterContext.Result = new JsonResult(result);
                    return;
                }
            }
            catch (Exception)
            {
                filterContext.Result = new JsonResult(result);
                return;
            }
            if (filterContext.HttpContext.Response != null &&
                filterContext.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                filterContext.Result = new JsonResult(result);
            }
        }
    }
}
