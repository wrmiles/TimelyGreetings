using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetings.Models
{
    public class GreetingViewModel
    {     
        // FROM GREETING ENTITY   
        public Int64 GreetingID { get; set; }

        public int OccassionTypeID { get; set; }

        public Int64 UserID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime DateDelivered { get; set; }

        public DateTime DateOpened { get; set; }

        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string TextBody { get; set; }

        // FROM RECIPIENT
                
        public Int64 RecipientID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }        

        public Int64 CreatedByUserID { get; set; }

        // FROM ATTACHMENT

        public Int64 AttachmentID { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentSize { get; set; }

        public string AttachmentData { get; set; }

        public string AttachmentURL { get; set; }

        

        

        


    }
}