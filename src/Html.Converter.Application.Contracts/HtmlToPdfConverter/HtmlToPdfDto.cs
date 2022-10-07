using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Html.Converter.HtmlToPdfConverter
{
    public class HtmlToPdfDto: IEntityDto<string>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public string Id { get ; set ; }
    }
}
