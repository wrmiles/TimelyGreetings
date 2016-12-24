using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class Upload
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public HttpPostedFileBase fileUpload { get; set; }

    }
}