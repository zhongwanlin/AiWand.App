using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiWand.Core.Domain;
using AiWand.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiWand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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


        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return new JsonResult(_userService.GetUser(id));
        }
    }
}