using Saptra.WebApi.Models;
using Saptra.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Data
{
    public class UsuarioData
    {
        /// <summary>
        /// Login de app
        /// </summary>
        /// <param name="usuario">Modelo de usuario</param>
        /// <returns>modelo de usaurio con información de registro</returns>
        public static mUsuarios GetLogin(mUsuarios usuario)
        {
            var loggedUser = new mUsuarios();
            try
            {
                using (var db  = new SaptraEntities())
                {
                    loggedUser = (from usr in db.mUsuarios
                                    join est in db.cEstatus on usr.EstatusId equals est.EstatusId
                                    join tes in db.cTipoEstatus on est.TipoEstatusId equals tes.TipoEstatusId
                                    join tfg in db.cTipoFiguras on usr.TipoFiguraId equals tfg.TipoFiguraId
                                    where est.NombreEstatus == Globals.EST_ACTIVO &&
                                          tes.nombreTipoEstatus == Globals.TES_BORRADO_LOGICO &&
                                        usr.LoginUsuario == usuario.LoginUsuario &&
                                        usr.PasswordUsuario == usuario.PasswordUsuario &&
                                        Globals.CAT_TIPO_FIGURA.Contains(tfg.DescripcionTipoFigura)
                                    select usr).FirstOrDefault();
                    loggedUser.cTipoFiguras1 = new cTipoFiguras(){DescripcionTipoFigura = loggedUser.cTipoFiguras1.DescripcionTipoFigura};
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                }
                return loggedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
