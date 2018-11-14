using System;

namespace Saptra.WebApi.Models
{
    public class LecturaCertificado
    {
        public LecturaCertificado()
        {
            CheckIn = new CheckIn();
            UsuarioCreacionId = 0;
            LecturaCertificadoId = 0;
        }

        public int RowId{ get; set; }
        public int LecturaCertificadoId{ get; set; }
        public String FechaCreacion{ get; set; }
        public int UsuarioCreacionId{ get; set; }
        public String FolioCertificado{ get; set; }
        public int EstatusId{ get; set; }
        public CheckIn CheckIn { get; set; }
        public String State{ get; set; }
        public String UUID{ get; set; }
    }
}
