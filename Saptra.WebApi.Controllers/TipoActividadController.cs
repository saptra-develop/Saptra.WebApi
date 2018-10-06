using Saptra.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Saptra.WebApi.Controllers
{
    [RoutePrefix("Saptra/TipoActividad")]
    public class TipoActividadController : ApiController
    {
        /// <summary>
        /// Obtener listado de Tipos de Acividades
        /// </summary>
        /// <returns>Lista de tipos de actividades</returns>
        [HttpGet]
        [Route("GetTipoActividades")]
        public IHttpActionResult GeTipoActividades()
        {
            try
            {
                return Json(new { Status = 1, Datos = TipoActividadData.GetTipoActividades(), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = "", Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
