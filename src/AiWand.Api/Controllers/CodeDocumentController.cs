using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiWand.Core;
using AiWand.Core.DTO.CodeDocument;
using AiWand.Service.CodeDocument;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiWand.Api.Controllers
{
    [Route("api/codeDocument")]
    [ApiController]
    public class CodeDocumentController : ControllerBase
    {
        private readonly ICodeDocumentService _codeDocumentService;

        public CodeDocumentController(ICodeDocumentService codeDocumentService)
        {
            _codeDocumentService = codeDocumentService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("generateRequest")]
        public ActionResult GenerateRequest([FromBody]BuildInput input)
        {
            Result result = new Result(true);
            try
            {
                _codeDocumentService.Build(input);

                result.Message = "请求已接受，等待生成文档";
            }
            catch (Exception ex)
            {
                result.IsSuccessed = false;
                result.Message = ex.Message;
            }
            return new JsonResult(result);
        }
    }
}