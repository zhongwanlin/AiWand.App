using System;
using System.Collections.Generic;
using System.IO;
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
    public class CodeDocumentController : BaseController
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
                result.Message = ex.ToString();
            }
            return new JsonResult(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public ActionResult GetCodeDocuments(string companyName, string language)
        {
            Result result = new Result(true);
            try
            {
                var docs = _codeDocumentService.GetCodeDocuments(companyName, language);
                result.Data = docs;
                result.Message = "获取文档成功";
            }
            catch (Exception ex)
            {
                result.IsSuccessed = false;
                result.Message = ex.Message;
            }
            return new JsonResult(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("download")]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}