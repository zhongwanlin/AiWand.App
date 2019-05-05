using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiWand.Core;
using AiWand.Core.DTO.Users;
using AiWand.Service.Login;
using AiWand.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiWand.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public ActionResult Login([FromBody]LoginInput input)
        {
            var user = _loginService.Login(input);

            Result result = new Result();
            if (user == null)
            {
                result.IsSuccessed = false;
                result.Message = "用户名或密码错误";
            }
            else
            {
                result.IsSuccessed = true;
                LoginOutput output = new LoginOutput { UserName = user.UserName, Token = "" };
                result.Data = output;
            }

            return new JsonResult(result);
        }
    }
}