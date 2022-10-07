using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Html.Converter.HtmlToPdfConverter
{
    public class HtmlToPdfConverterAppService : CrudAppService<
           HtmlToPdfEntity,
           HtmlToPdfDto,
           string,
           PagedAndSortedResultRequestDto,
           CreateUpdateHtmlToPdfDto>,
           IHtmlToPdfConverterAppService
    {
        private readonly int _expirationInterval;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private IRepository<HtmlToPdfEntity, string> _repository;
        public HtmlToPdfConverterAppService(IRepository<HtmlToPdfEntity, string> repository,
            ILoggerFactory loggerFactory,
            IConfiguration configuration) : base(repository)
        {
            _logger = loggerFactory.CreateLogger<HtmlToPdfConverterAppService>();
            _repository = repository;
            _configuration = configuration;
            int.TryParse(_configuration["ExpirationIntervalInMinutes"], out int expirationInterval);
            if (expirationInterval != 0)
            {
                _expirationInterval = expirationInterval;
            }
            else
            {
                _expirationInterval = 5;
            }
        }

        public async Task<HtmlToPdfDownloadResponceDto> GetConvertedFileByNameAsync(string fileName)
        {
            try
            {
                var file = await _repository.GetAsync(fileName);
                if (file != null)
                {
                    using (var filestream = new FileStream(file.FilePath, FileMode.Open))
                    {
                        var result = await filestream.GetAllBytesAsync();

                        return new HtmlToPdfDownloadResponceDto { Content = result, FileName = file.FileName };
                    }

                }
                else
                {
                    throw new FileNotFoundException(fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public async Task<bool> ConvertFileByNameAsync(string taskId)
        {
           
            try
            {
                var file = await _repository.GetAsync(taskId);
                var FilePath = file.FilePath;
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                await using var page = await browser.NewPageAsync();
                await page.GoToAsync("file:///" + FilePath);
                await page.PdfAsync(Path.Combine(Path.GetTempPath(), taskId + "converted.pdf"));


                file.FileName = taskId + "converted.pdf";
                    file.FilePath = Path.Combine(Path.GetTempPath(), taskId + "converted.pdf");
                    file.CreationDate = DateTime.UtcNow;
                var res = await _repository.UpdateAsync(file);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return false;
            }
        }
        public async Task<bool> UploadFileForConversionAsync(HtmlToPdfRequest input)
        {
            var tempPathDirectory = Path.GetTempPath();
            var expiredFilesList = await _repository.GetListAsync(
                x => x.CreationDate <= DateTime.UtcNow.AddMinutes(-_expirationInterval));
            if (expiredFilesList != null)
            {
                foreach (var item in expiredFilesList)
                {
                    try
                    {
                        File.Delete(item.FilePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogException(ex);
                    }
                }
            }

            var stream = input.FileStream;
            var streamName = input.FileStream.FileName;
            var fileName = input.FileName + ".html";
            var FilePath = Path.Combine(tempPathDirectory, fileName);
            try
            {
                using (var fileStream = new FileStream(FilePath, FileMode.OpenOrCreate))
                {

                    await stream.GetStream().CopyToAsync(fileStream);

                    fileStream.Flush();
                    fileStream.Dispose();
                }

                var inserdRes = await _repository.InsertAsync(
                    new HtmlToPdfEntity(input.FileName, fileName, DateTime.UtcNow, FilePath));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return true;
        }

    }
}
