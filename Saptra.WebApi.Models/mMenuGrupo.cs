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
    
    public partial class mMenuGrupo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mMenuGrupo()
        {
            this.dModulos = new HashSet<dModulos>();
        }
    
        public int MenuGrupoId { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<int> EstatusId { get; set; }
        public string NombreMenuGrupo { get; set; }
        public string IconGrupo { get; set; }
        public Nullable<int> OrdenGrupo { get; set; }
    
        public virtual cEstatus cEstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dModulos> dModulos { get; set; }
    }
}
