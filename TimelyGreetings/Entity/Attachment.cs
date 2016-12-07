using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimelyGreetings.Entity
{
    public class Attachment
    {
        [Key]
        public Int64 AttachmentID { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentSize { get; set; }

        public string AttachmentData { get; set; }

        public string AttachmentURL { get; set; }

        public DateTime DateCreated { get; set; }

        public Int64 MessageID { get; set; }

        public Int64 GreetingID { get; set; }
    }
}