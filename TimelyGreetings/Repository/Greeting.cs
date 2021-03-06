//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimelyGreetings.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Greeting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Greeting()
        {
            this.Attachments = new HashSet<Attachment>();
            this.Recipients = new HashSet<Recipient>();
        }
    
        public long GreetingID { get; set; }
        public int OccassionTypeID { get; set; }
        public long UserID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateToSend { get; set; }
        public Nullable<System.DateTime> DateDelivered { get; set; }
        public Nullable<System.DateTime> DateOpened { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual OccassionType OccassionType { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipient> Recipients { get; set; }
    }
}
