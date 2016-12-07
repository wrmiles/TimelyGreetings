using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class OccassionType
    {
        [Key]
        public int OccassionTypeID { get; set; }

        [Required(ErrorMessage = "Occassion Type Name is required.")]
        public string OccassionTypeName { get; set; }
    }
}