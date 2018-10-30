using Saptra.WebApi.Models;
using Saptra.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Data
{
    public class PeriodoData
    {
        /// <summary>
        /// Obtener Periodo actual
        /// </summary>
        /// <returns>periodo actual en base a fecha del sistema</returns>
        public static List<cPeriodos> GetPeriodo()
        {
            var periodos = new List<cPeriodos>();
            try
            {
                using (var db = new SaptraEntities())
                {
                    periodos = (from per in db.cPeriodos
                                  join est in db.cEstatus on per.EstatusId equals est.EstatusId
                                  join tes in db.cTipoEstatus on est.TipoEstatusId equals tes.TipoEstatusId
                                  where est.NombreEstatus == Globals.EST_ACTIVO &&
                                        tes.nombreTipoEstatus == Globals.TES_BORRADO_LOGICO &&
                                        (DateTime.Now >= per.FechaInicio && DateTime.Now <= per.FechaFin)
                                select per).ToList();
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
                return periodos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
