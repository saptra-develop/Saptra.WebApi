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
    
    public partial class mResetPassword
    {
        public System.Guid Id { get; set; }
        public int UsuarioId { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Liga { get; set; }
        public Nullable<int> EstatusId { get; set; }
    
        public virtual mUsuarios mUsuarios { get; set; }
    }
}
