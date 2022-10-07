
using Html.Converter.HtmlToPdfConverter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Content;
using Volo.Abp.Guids;
using static System.Net.WebRequestMethods;

namespace Html.Converter.Web.Pages;

public class IndexModel : ConverterPageModel
{
    [BindProperty]
    public UploadFileDto FileDto { get; set; }

    [ReadOnlyInput]
    [BindProperty]
    public string GeneratedFileId { get; set; } = "";
    private IHtmlToPdfConverterAppService _converterAppService;
    private IGuidGenerator _guidGenerator;
    public IndexModel(IGuidGenerator generator, IHtmlToPdfConverterAppService converterAppService)
    {
        _guidGenerator = generator;
        _converterAppService = converterAppService;
        GeneratedFileId = "";
    }

    public void OnGet()
    {
        var taskId = HttpContext.Request.Cookies["taskId"];
        if (string.IsNullOrEmpty(taskId))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.UtcNow.AddSeconds(5);
            GeneratedFileId = _guidGenerator.Create().ToString();
            HttpContext.Response.Cookies.Append("taskId", GeneratedFileId, cookieOptions);
        }
        else
        {
            GeneratedFileId = taskId;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CookieOptions cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.UtcNow.AddSeconds(5);
        GeneratedFileId = _guidGenerator.Create().ToString();
        HttpContext.Response.Cookies.Delete("taskId");
        HttpContext.Response.Cookies.Append("taskId", GeneratedFileId, cookieOptions);
        using (var memoryStream = new MemoryStream(FileDto.File.GetAllBytes()))
        {
            if (FileDto.File != null)
            {
                await _converterAppService.UploadFileForConversionAsync(new HtmlToPdfRequest
                { FileName = GeneratedFileId, FileStream = new RemoteStreamContent(memoryStream, GeneratedFileId) });

            }

            memoryStream.Dispose();
        }

        return Page();

    }

    public class UploadFileDto
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }
    }
}
