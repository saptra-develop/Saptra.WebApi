//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Saptra.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mBitacora
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> ExecuteDate { get; set; }
        public string Ip { get; set; }
        public string Username { get; set; }
        public string Reqtype { get; set; }
        public string Reqdata { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
