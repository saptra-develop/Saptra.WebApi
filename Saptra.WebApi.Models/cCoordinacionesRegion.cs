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
    
    public partial class cCoordinacionesRegion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cCoordinacionesRegion()
        {
            this.cCoordinacionesZona = new HashSet<cCoordinacionesZona>();
            this.mCoordinacionRegionZonaUsuario = new HashSet<mCoordinacionRegionZonaUsuario>();
        }
    
        public int CoordinacionRegionId { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public string DescripcionCoordinacionRegion { get; set; }
        public int EstatusId { get; set; }
    
        public virtual cEstatus cEstatus { get; set; }
        public virtual mUsuarios mUsuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cCoordinacionesZona> cCoordinacionesZona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mCoordinacionRegionZonaUsuario> mCoordinacionRegionZonaUsuario { get; set; }
    }
}
