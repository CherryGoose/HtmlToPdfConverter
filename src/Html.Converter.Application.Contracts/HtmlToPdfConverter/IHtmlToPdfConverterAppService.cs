using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace Html.Converter.HtmlToPdfConverter
{
    public interface IHtmlToPdfConverterAppService
    {
        public Task<bool> UploadFileForConversionAsync(HtmlToPdfRequest input);
        public Task<bool> ConvertFileByNameAsync(string taskId);
        public Task<HtmlToPdfDownloadResponceDto> GetConvertedFileByNameAsync(string fileName);


    }
}
