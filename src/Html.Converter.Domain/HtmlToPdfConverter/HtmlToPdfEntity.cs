using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Html.Converter.HtmlToPdfConverter
{
    public class HtmlToPdfEntity : Entity<string>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        private HtmlToPdfEntity() { }

        public HtmlToPdfEntity(string id, string fileName, DateTime creatiodDate, string filePath)
        {
            Id = id;
            FileName = fileName;
            CreationDate = creatiodDate;
            FilePath = filePath;
        }
    }
}
