using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class User
    {
        [Key]
        public Int64 UserID { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User PW is required.")]
        public string PW { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string EmailAddress { get; set; }
        public Int16 Status { get; set; }
        public DateTime DateCreated { get; set; }
        public Int16 UserType { get; set; }
    }
}