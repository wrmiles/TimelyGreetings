using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimelyGreetings.Entity
{
    public class Greeting
    {
        [Key]
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

    }
}