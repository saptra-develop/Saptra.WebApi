//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Saptra.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mCoordinacionZonaUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mCoordinacionZonaUsuario()
        {
            this.mNotificaciones = new HashSet<mNotificaciones>();
        }
    
        public int CordinacionZonaUsuarioId { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public int CoordinacionZonaId { get; set; }
        public int UsuarioId { get; set; }
        public Nullable<bool> JefeCoordinacionZona { get; set; }
    
        public virtual cCoordinacionesZona cCoordinacionesZona { get; set; }
        public virtual mUsuarios mUsuarios { get; set; }
        public virtual mUsuarios mUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mNotificaciones> mNotificaciones { get; set; }
    }
}
