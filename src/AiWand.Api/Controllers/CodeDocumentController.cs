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
                string doc = _codeDocumentService.Build(input);
                result.Data = doc;
                result.Message = "文档生成成功";
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