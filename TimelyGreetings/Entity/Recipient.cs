using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimelyGreetings.Entity
{
    public class Recipient
    {
        [Key]
        public Int64 RecipientID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateCreated { get; set; }

        public Int64 CreatedByUserID { get; set; }

        public Int64 GreetingID { get; set; }

    }
}