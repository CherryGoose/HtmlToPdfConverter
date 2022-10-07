using System;
using System.Collections.Generic;
using System.Text;

namespace Html.Converter.HtmlToPdfConverter
{
    public class CreateUpdateHtmlToPdfDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
       
    }
}
