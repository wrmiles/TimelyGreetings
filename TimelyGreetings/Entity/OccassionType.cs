using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimelyGreetings.Entity
{
    public class OccassionType
    {
        [Key]
        public int OccassionTypeID { get; set; }
        public string OccassionTypeName { get; set; }
    }
}