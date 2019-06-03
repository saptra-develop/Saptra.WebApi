using Saptra.WebApi.Models;
using Saptra.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Data
{
    public class CheckInData
    {
        /// <summary>
        /// Obtener listado de CheckIns realizados (incluye actividades especiales)
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        public static List<LecturaCertificado> GetCheckInsRealizados(int PeriodoId, int UsuarioId)
        {
            var checks = new List<LecturaCertificado>();
            try
            {
                using (var db = new SaptraEntities())
                {
                    var checkIns = (from chk in db.mCheckIn
                                   join pld in db.dDetallePlanSemanal on chk.DetallePlanId equals pld.DetallePlanId
                                   join ple in db.mPlanSemanal on pld.PlanSemanalId equals ple.PlanSemanalId
                                   where ple.UsuarioCreacionId == UsuarioId &&
                                         ple.PeriodoId == PeriodoId
                                   select chk).ToList();

                    if (checkIns.Count() > 0) {

                        checkIns.ForEach(i => {
                            var _cert = db.mLecturaCertificados
                                        .Where(c => c.CheckInId == i.CheckInId).FirstOrDefault();
                            checks.Add(new LecturaCertificado() {
                                //CheckIn
                                CheckIn = new CheckIn()
                                {
                                    CheckInId = i.CheckInId.ToString(),
                                    FechaCreacion = i.FechaCreacion.ToLongDateString(),
                                    UsuarioCreacionId = i.UsuarioCreacionId,
                                    Coordenadas = i.Coordenadas,
                                    dDetallePlanSemanal = new dDetallePlanSemanal() { DetallePlanId = i.DetallePlanId },
                                    Incidencias = i.Incidencias,
                                    ImageData = Globals.GetImgBase64(i.FotoIncidencia),
                                    FotoIncidencia = i.FotoIncidencia,
                                    State = "S",
                                    UUID = ""                                
                                },
                                //LecturaCertificado
                                LecturaCertificadoId = _cert != null ? _cert.LecturaCertificadoId : 0,
                                FechaCreacion = _cert != null ? _cert.FechaCreacion.ToLongDateString() : "",
                                UsuarioCreacionId = _cert != null ? _cert.UsuarioCreacionId : 0,
                                FolioCertificado = _cert != null ? _cert.FolioCertificado : "",
                                State = "S",
                                UUID = ""
                            });
                        });
                    }
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            return checks;
        }

        /// <summary>
        /// Post de CheckIns recibidos desde el móvil
        /// </summary>
        /// <param name="checks">Lista de checkins a sincronizar</param>
        /// <returns>Lista de checkins sincronizados</returns>
        public static List<LecturaCertificado> PostCheckIns(List<LecturaCertificado> checks)
        {
            try
            {
                if (checks.Count() > 0)
                {
                    using (var db = new SaptraEntities())
                    {
                        foreach (var obj in checks)
                        {
                            //Validar si checkin ya ha sido enviado anteriormente
                            var existe_checkin = db.mCheckIn.Where(x=>x.UUID.Equals(obj.CheckIn.UUID)).FirstOrDefault();
                            if(existe_checkin != null)
                            {
                                obj.CheckIn.State = "S";
                                obj.CheckIn.ImageData = "";
                                obj.CheckIn.CheckInId = existe_checkin.CheckInId.ToString();
                                obj.State = "S";
                                var Certificado = db.mLecturaCertificados.Where(x=>x.CheckInId == existe_checkin.CheckInId).FirstOrDefault();
                                obj.LecturaCertificadoId = Certificado != null ? Certificado.LecturaCertificadoId : 0;
                                continue;
                            }

                            var new_check = new mCheckIn()
                            {
                                FechaCreacion = DateTime.Parse(obj.CheckIn.FechaCreacion),
                                UsuarioCreacionId = obj.CheckIn.UsuarioCreacionId,
                                Coordenadas = obj.CheckIn.Coordenadas,
                                DetallePlanId = obj.CheckIn.dDetallePlanSemanal.DetallePlanId,
                                Incidencias = obj.CheckIn.Incidencias,
                                FotoIncidencia = Globals.PathImage(obj.CheckIn.ImageData),
                                UUID = obj.CheckIn.UUID
                                
                            };
                            db.mCheckIn.Add(new_check);
                            db.SaveChanges();
                            if (new_check.CheckInId > 0)
                            {
                                obj.CheckIn.ImageData = "";
                                obj.CheckIn.State = "S";
                                obj.CheckIn.CheckInId = new_check.CheckInId.ToString();
                                if (obj.UUID.ToString().Equals(obj.CheckIn.UUID))
                                {
                                    var new_certificado = new mLecturaCertificados()
                                    {
                                        FechaCreacion = DateTime.Parse(obj.FechaCreacion),
                                        UsuarioCreacionId = obj.UsuarioCreacionId,
                                        FolioCertificado = obj.FolioCertificado,
                                        CheckInId = new_check.CheckInId,
                                        EstatusId = 5,
                                        UUID = obj.UUID
                                    };
                                    db.mLecturaCertificados.Add(new_certificado);
                                    db.SaveChanges();
                                    if (new_certificado.LecturaCertificadoId > 0)
                                    {
                                        obj.State = "S";
                                        obj.LecturaCertificadoId = new_certificado.LecturaCertificadoId;

                                        //Insertar en mysql
                                        var user = db.mUsuarios.Where(u => u.UsuarioId == new_certificado.UsuarioCreacionId).FirstOrDefault();
                                        if(user != null)
                                        {
                                            //Se insenta insertar en Mysql, el resultado del intento se actualiza en la entidad
                                            bool _insert = MySQLDB.PostCheckIn(new_certificado.FolioCertificado, user.RFCUsuario);
                                            var _certificado = db.mLecturaCertificados.Where(s=>s.LecturaCertificadoId == new_certificado.LecturaCertificadoId).FirstOrDefault();
                                            _certificado.SincronizadoMySQL = _insert;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return checks;
        }
    }
}
