using Saptra.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Saptra.WebApi.Controllers
{
    [RoutePrefix("Saptra/Periodo")]
    public class PeriodoController : ApiController
    {
        /// <summary>
        /// Obtener Periodo actual.
        /// </summary>
        /// <returns>periodo actual en base a fecha del sistema</returns>
        [HttpGet]
        [Route("GetPeriodo")]
        public IHttpActionResult GetPerirodo()
        {
            try
            {
                return Json(new { Status = 1, Datos = PeriodoData.GetPeriodo(), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = "", Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
