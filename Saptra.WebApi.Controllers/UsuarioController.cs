﻿using Saptra.WebApi.Data;
using Saptra.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Saptra.WebApi.Controllers
{
    [RoutePrefix("Saptra/Usuario")]
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// Login de app
        /// </summary>
        /// <param name="usuario">Modelo de usuario</param>
        /// <returns>modelo de usaurio con información de registro</returns>
        [HttpPost]
        [Route("GetLogin")]
        public IHttpActionResult GetLogin(mUsuarios usuario)
        {
            try
            {
                return Json(new { Status = 1, Datos = UsuarioData.GetLogin(usuario), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = usuario, Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost]
        [Route("PostSession")]
        public IHttpActionResult PostSession([FromUri]int UsuarioId, [FromUri]bool Logged)
        {
            try
            {
                return Json(new { Status = 1, Datos = UsuarioData.PostSession(UsuarioId, Logged), Error = "", StackTrace = "" });
            }
            catch (HttpResponseException ex)
            {
                return Json(new { Status = 0, Datos = UsuarioId, Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        /// <summary>
        /// Obtener fecha y hora del servidor donde se monta
        /// </summary>
        /// <returns>Fecha y hora</returns>
        [HttpGet]
        [Route("GetCurrentServerTime")]
        public IHttpActionResult getCurrentTime()
        {
            return Json(new { Time = DateTime.Now, Version_App = "AR-280919" });
        }
    }
}
