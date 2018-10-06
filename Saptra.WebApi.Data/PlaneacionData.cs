﻿using Saptra.WebApi.Models;
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
                                   join tac in db.cTipoActividades on pld.TipoActividadId equals tac.TipoActividadId
                                   join es1 in db.cEstatus on ple.EstatusId equals es1.EstatusId
                                   join te1 in db.cTipoEstatus on es1.TipoEstatusId equals te1.TipoEstatusId
                                   join es2 in db.cEstatus on tac.EstatusId equals es2.EstatusId
                                   join te2 in db.cTipoEstatus on es2.TipoEstatusId equals te2.TipoEstatusId
                                   where (es1.NombreEstatus == Globals.EST_VALIDADO &&
                                         te1.nombreTipoEstatus == Globals.TES_PLAN_SEMANAL) &&
                                         (es2.NombreEstatus == Globals.EST_ACTIVO &&
                                         te2.nombreTipoEstatus == Globals.TES_BORRADO_LOGICO) &&
                                         tac.RequiereCheckIn == true &&
                                         ple.UsuarioCreacionId == UsuarioId
                                   select ple).Distinct().ToList();
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
                                   join tac in db.cTipoActividades on pld.TipoActividadId equals tac.TipoActividadId
                                   join es1 in db.cEstatus on pld.EstatusId equals es1.EstatusId
                                   join te1 in db.cTipoEstatus on es1.TipoEstatusId equals te1.TipoEstatusId
                                   join es2 in db.cEstatus on tac.EstatusId equals es2.EstatusId
                                   join te2 in db.cTipoEstatus on es2.TipoEstatusId equals te2.TipoEstatusId
                                   where (es1.NombreEstatus == Globals.EST_VALIDADO &&
                                         te1.nombreTipoEstatus == Globals.TES_PLAN_SEMANAL) &&
                                         (es2.NombreEstatus == Globals.EST_ACTIVO &&
                                         te2.nombreTipoEstatus == Globals.TES_BORRADO_LOGICO) &&
                                         pld.UsuarioCreacionId == UsuarioId &&
                                         tac.RequiereCheckIn == true
                                   select pld).ToList();
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