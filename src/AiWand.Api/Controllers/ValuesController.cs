using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiWand.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace AiWand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //return new JsonResult(_userService.GetUser("1"));
            return new JsonResult(new { Code = 200, Message = "请求成功" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
