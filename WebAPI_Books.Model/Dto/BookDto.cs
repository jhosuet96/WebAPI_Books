using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_Books.Model.Dto
{
    public class BookDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public string excerpt { get; set; }
        public DateTime publishDate { get; set; }
    }
}
