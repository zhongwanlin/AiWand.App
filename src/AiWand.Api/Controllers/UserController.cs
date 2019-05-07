using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiWand.Core;
using AiWand.Core.Attributes;
using AiWand.Core.Domain;
using AiWand.Core.DTO.Users;
using AiWand.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiWand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public ActionResult Add([FromBody] User user)
        {
            _userService.Add(user);

            return new JsonResult(new { Code = "200", Message = "新增成功" });
        }

        [UnAuth]
        [HttpPost("register")]
        public ActionResult Register([FromBody] UserRegister userRegister)
        {
            Result result = new Result(true);
            try
            {
                var user = _userService.Register(userRegister);
                result.Data = user;
                result.Message = "注册成功";
            }
            catch (Exception ex)
            {
                result.IsSuccessed = false;
                result.Message = ex.Message;
            }
            return new JsonResult(result);
        }


        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return new JsonResult(_userService.GetUser(id));
        }
    }
}