using AiWand.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiWand.Api
{
    [TypeFilter(typeof(LoginAttribute))]
    public class BaseController : ControllerBase
    {

    }
}
