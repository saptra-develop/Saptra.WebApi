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
    
    public partial class mLecturaCertificados
    {
        public int LecturaCertificadoId { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public string FolioCertificado { get; set; }
        public int EstatusId { get; set; }
        public int CheckInId { get; set; }
        public Nullable<bool> SincronizadoMySQL { get; set; }
    
        public virtual cEstatus cEstatus { get; set; }
        public virtual mCheckIn mCheckIn { get; set; }
        public virtual mUsuarios mUsuarios { get; set; }
    }
}
