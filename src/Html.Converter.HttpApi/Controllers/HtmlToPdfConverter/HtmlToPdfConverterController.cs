using Html.Converter.HtmlToPdfConverter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace Html.Converter.Controllers.HtmlToPdfConverter
{
    [RequestSizeLimit(262144000)]
    [Route("api/conventer")]
    public class HtmlToPdfConverterController : ConverterController, IHtmlToPdfConverterAppService
    {
        private IHtmlToPdfConverterAppService _converterAppService;

        public HtmlToPdfConverterController(IHtmlToPdfConverterAppService converterAppService)
        { 
            _converterAppService = converterAppService;
        }
        [HttpPost("convert")]

        public async Task<bool> ConvertFileByNameAsync(string taskId)
        {
          return await _converterAppService.ConvertFileByNameAsync(taskId);
        }
        [HttpGet("download")]
        public async Task<IActionResult> GetConvertedFileByNameAsync(string fileName)
        {
            var result = await _converterAppService.GetConvertedFileByNameAsync(fileName);
            return File(result.Content, "application/octet-stream", result.FileName);
        }

        Task<HtmlToPdfDownloadResponceDto> IHtmlToPdfConverterAppService.GetConvertedFileByNameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        [HttpPost("upload")]
        async Task<bool> IHtmlToPdfConverterAppService.UploadFileForConversionAsync(HtmlToPdfRequest input)
        {
            return await _converterAppService.UploadFileForConversionAsync(input);
        }
      
    }
}
