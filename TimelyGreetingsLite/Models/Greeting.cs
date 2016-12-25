using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class Greeting
    {
        /***
         * @OccassionTypeID int,
	@UserID bigint,
	@DateToSend datetime,
	@DateDelivered datetime,	
	@DateOpened datetime,
	@Subject varchar(100),
	@BodyText varchar(max)
         * 
         * ***/
        [Key]
        public Int64 GreetingID { get; set; }

        
        [Required(ErrorMessage = "Occassion Type is required.")]
        public int OccassionTypeID { get; set; }

        public Int64 UserID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Send On")]
        public DateTime? DateToSend { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Delivered")]
        public DateTime? DateDelivered { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Opened")]
        public DateTime? DateOpened { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Text Body is required.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Text")]
        public string BodyText { get; set; }
        public List<OccassionType> OccassionTypes { get; set; }

        public Int64? GreetingTemplateID { get; set; }

        public string GreetingTemplateName { get; set; }

        public List<GreetingTemplate> GreetingTemplates { get; set; }

        [DisplayName("Occassion")]
        public string OccassionTypeName { get; set; }

    }
}