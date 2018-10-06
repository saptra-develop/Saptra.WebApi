using Saptra.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Saptra.WebApi.Controllers
{
    [RoutePrefix("Saptra/Planeacion")]
    public class PlaneacionController : ApiController
    {
        /// <summary>
        /// Obtener Planeacion Semanal
        /// </summary>
        /// <param name="PeriodoId">Id del periodo</param>
        /// <param name="UsuarioId">Id de usuario</param>
        /// <returns>Lista de planeacion semanal por periodo y usuario</returns>
        [HttpGet]
        [Route("GetPlaneacionSemanal")]
        public IHttpActionResult GetPlaneacionSemanal(int PeriodoId, int UsuarioId)
        {
            try
            {
                return Json(new { Status = 1, Datos = PlaneacionData.GetPlanSemanal(PeriodoId, UsuarioId), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = "", Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Obtener Detalle de Planeacion Semanal
        /// </summary>
        /// <param name="PeriodoId">Id del periodo</param>
        /// <param name="UsuarioId">Id de usuario</param>
        /// <returns>Lista de detalle de planeacion semanal por periodo y usuario</returns>
        [HttpGet]
        [Route("GetDetallePlaneacionSemanal")]
        public IHttpActionResult GetDetallePlaneacionSemanal(int PeriodoId, int UsuarioId)
        {
            try
            {
                return Json(new { Status = 1, Datos = PlaneacionData.GetDetallePlanSemanal(PeriodoId, UsuarioId), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = "", Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
