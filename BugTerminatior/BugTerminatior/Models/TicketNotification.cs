//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BugTerminatior.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserName { get; set; }
    
        public virtual BTUser BTUser { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
