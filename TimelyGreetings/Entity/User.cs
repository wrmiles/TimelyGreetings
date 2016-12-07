using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TimelyGreetings.Entity
{
    public class User
    {

        [Key]
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string PW { get; set; }
        public string FirstName { get; set; }
        public string LastName   { get; set; }
        public string EmailAddress { get; set; }
        public Int16 Status { get; set; }
        public DateTime DateCreated { get; set; }
        public Int16 UserType { get; set; }

    }
}