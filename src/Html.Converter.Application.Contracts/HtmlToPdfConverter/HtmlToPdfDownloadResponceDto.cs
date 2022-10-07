using System;
using System.Collections.Generic;
using System.Text;

namespace Html.Converter.HtmlToPdfConverter
{
    public class HtmlToPdfDownloadResponceDto
    {
        public byte[] Content { get; set; }

        public string FileName { get; set; }
    }
}
