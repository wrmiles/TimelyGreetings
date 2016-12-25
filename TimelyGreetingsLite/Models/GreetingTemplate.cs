using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimelyGreetingsLite.Models
{
    public class GreetingTemplate
    {
        [Key]
        public Int64 GreetingTemplateID { get; set; }
        public string GreetingTemplateName { get; set; }
        public int OccassionTypeID { get; set; }
        public string FrontCoverBackgroundColor { get; set; }
        public string FrontCoverBackgroundPatternFilePathName { get; set; }
        public string FrontCoverImageFilePathName { get; set; }
        public string InsideBackgroundColor { get; set; }
        public string InsideBackgroundPatternFilePathName { get; set; }
        public string InsideImageFilePathName { get; set; }
        public string FrontCoverFont { get; set; }
        public string FrontCoverFontSize { get; set; }
        public string FrontCoverFontColor { get; set; }
        public string FrontCoverFontWeight { get; set; }
        public string InsideFont { get; set; }
        public string InsideFontSize { get; set; }
        public string InsideFontColor { get; set; }
        public string InsideFontWeight { get; set; }
        public string MusicPathFileName { get; set; }

        
    }
}