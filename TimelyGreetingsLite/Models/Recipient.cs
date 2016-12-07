using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class Recipient
    {
        [Key]
        public Int64 RecipientID { get; set; }

        [Required(ErrorMessage = "Recipient First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Recipient Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Recipient Email is required.")]
        public string EmailAddress { get; set; }

        public DateTime DateCreated { get; set; }

        public Int64 CreatedByUserID { get; set; }

        public Int64 GreetingID { get; set; }
    }
}