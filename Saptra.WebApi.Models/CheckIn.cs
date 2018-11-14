using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Models
{
    public class CheckIn
    {
        public CheckIn()
        {
            dDetallePlanSemanal = new dDetallePlanSemanal();
        }

        public int RowId { get; set; }
        public String CheckInId { get; set; }
        public String FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public String Coordenadas { get; set; }
        public dDetallePlanSemanal dDetallePlanSemanal { get; set; }
        public String Incidencias { get; set; }
        public String FotoIncidencia { get; set; }
        public String ImageData { get; set; }
        public String State { get; set; }
        public String UUID { get; set; }
    }
}
