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
    
    public partial class mCheckIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mCheckIn()
        {
            this.mLecturaCertificados = new HashSet<mLecturaCertificados>();
        }
    
        public int CheckInId { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public string Coordenadas { get; set; }
        public int DetallePlanId { get; set; }
        public string Incidencias { get; set; }
        public string FotoIncidencia { get; set; }
        public string UUID { get; set; }
        public string FotoRutaLocal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mLecturaCertificados> mLecturaCertificados { get; set; }
        public virtual dDetallePlanSemanal dDetallePlanSemanal { get; set; }
        public virtual mUsuarios mUsuarios { get; set; }
    }
}
