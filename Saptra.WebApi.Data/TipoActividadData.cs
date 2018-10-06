using Saptra.WebApi.Models;
using Saptra.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Data
{
    public class TipoActividadData
    {
        /// <summary>
        /// Obtener listado de Tipos de Acividades
        /// </summary>
        /// <returns>Lista de tipos de actividades</returns>
        public static List<cTipoActividades> GetTipoActividades()
        {
            var tipoActividad = new List<cTipoActividades>();
            try
            {
                using (var db = new SaptraEntities())
                {
                    tipoActividad = (from tac in db.cTipoActividades 
                                     join est in db.cEstatus on tac.EstatusId equals est.EstatusId
                                     join tes in db.cTipoEstatus on est.TipoEstatusId equals tes.TipoEstatusId
                                     where (est.NombreEstatus == Globals.EST_ACTIVO &&
                                        tes.nombreTipoEstatus == Globals.TES_BORRADO_LOGICO) &&
                                        tac.RequiereCheckIn == true
                                    select tac).ToList();
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
                return tipoActividad;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
