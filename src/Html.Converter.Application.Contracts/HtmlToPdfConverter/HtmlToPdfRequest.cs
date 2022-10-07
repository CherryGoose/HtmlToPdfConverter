using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Content;

namespace Html.Converter.HtmlToPdfConverter
{
    public class HtmlToPdfRequest
    {
        [JsonProperty("fileStream")]
        public IRemoteStreamContent FileStream { get; set; }
        [JsonProperty("filename")]
        public string FileName { get; set; }
    }
}
