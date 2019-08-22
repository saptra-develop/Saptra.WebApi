using Saptra.WebApi.Models;
using Saptra.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Data
{
    public class PlaneacionData
    {
        /// <summary>
        /// Obtener Planeacion Semanal
        /// </summary>
        /// <param name="PeriodoId">Id del periodo</param>
        /// <param name="UsuarioId">Id de usuario</param>
        /// <returns>Lista de planeacion semanal por periodo y usuario</returns>
        public static List<mPlanSemanal> GetPlanSemanal(int PeriodoId, int UsuarioId)
        {
            var planSemanal = new List<mPlanSemanal>();
            try
            {
                using (var db = new SaptraEntities())
                {
                    planSemanal = (from ple in db.mPlanSemanal
                                   join pld in db.dDetallePlanSemanal on ple.PlanSemanalId equals pld.PlanSemanalId
                                   where ple.UsuarioCreacionId == UsuarioId &&
                                         ple.PeriodoId == PeriodoId &&
                                         ple.EstatusId == 2
                                   select ple).Distinct().ToList();
                    planSemanal.ForEach(i=>{
                        i.cPeriodos = new cPeriodos() { PeriodoId = i.PeriodoId, DecripcionPeriodo = i.cPeriodos.DecripcionPeriodo };
                    });
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
                return planSemanal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtener Detalle de Planeacion Semanal
        /// </summary>
        /// <param name="PeriodoId">Id del periodo</param>
        /// <param name="UsuarioId">Id de usuario</param>
        /// <returns>Lista de detalle de planeacion semanal por periodo y usuario</returns>
        public static List<dDetallePlanSemanal> GetDetallePlanSemanal(int PeriodoId, int UsuarioId)
        {
            var detallePlanSemanal = new List<dDetallePlanSemanal>();
            try
            {
                using (var db = new SaptraEntities())
                {
                    detallePlanSemanal = (from pld in db.dDetallePlanSemanal
                                          join pls in db.mPlanSemanal on pld.PlanSemanalId equals pls.PlanSemanalId
                                           where pls.UsuarioCreacionId == UsuarioId &&
                                                 pls.PeriodoId == PeriodoId &&
                                                 pls.EstatusId == 2 &&
                                                 pld.cTipoActividades.RequiereCheckIn == true
                                           select pld).ToList();
                    detallePlanSemanal.ForEach(i=> {
                        i.mPlanSemanal = new mPlanSemanal() {PlanSemanalId = i.PlanSemanalId, DescripcionPlaneacion = i.mPlanSemanal.DescripcionPlaneacion };
                        i.cTipoActividades = new cTipoActividades() {
                            TipoActividadId = i.cTipoActividades.TipoActividadId,
                            NombreActividad = i.cTipoActividades.NombreActividad,
                            DescripcionActividad = i.cTipoActividades.DescripcionActividad,
                            RequiereCheckIn = i.cTipoActividades.RequiereCheckIn,
                            ActividadEspecial = i.cTipoActividades.ActividadEspecial,
                            Mensaje = i.cTipoActividades.Mensaje
                        };
                    });
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
                return detallePlanSemanal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
