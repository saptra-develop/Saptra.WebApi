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
                    var user = (from usr in db.mUsuarios
                                    where usr.cEstatus1.NombreEstatus == Globals.EST_ACTIVO &&
                                        usr.LoginUsuario == usuario.LoginUsuario &&
                                        usr.PasswordUsuario == usuario.PasswordUsuario &&
                                        Globals.CAT_TIPO_FIGURA.Contains(usr.cTipoFiguras1.DescripcionTipoFigura)
                                    select usr).FirstOrDefault();
                    if (user != null)
                    {
                        loggedUser = user;
                        loggedUser.cTipoFiguras1 = new cTipoFiguras() { DescripcionTipoFigura = loggedUser.cTipoFiguras1.DescripcionTipoFigura };
                        db.Configuration.LazyLoadingEnabled = false;
                        db.Configuration.ProxyCreationEnabled = false;
                    }
                }
                return loggedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Notifica si el usuario cerrò sesiòn desde el dispositivo
        /// </summary>
        /// <param name="UsuarioId">Usuario</param>
        /// <param name="Logged">Estatus de login</param>
        public static mUsuarios PostSession(int UsuarioId, bool Logged) {
            try
            {
                var _usr = new mUsuarios();
                using (var db = new SaptraEntities())
                {
                    var user = (from usr in db.mUsuarios
                                where usr.cEstatus1.NombreEstatus == Globals.EST_ACTIVO &&
                                    usr.UsuarioId == UsuarioId &&
                                    Globals.CAT_TIPO_FIGURA.Contains(usr.cTipoFiguras1.DescripcionTipoFigura)
                                select usr).FirstOrDefault();
                    if (user != null)
                    {

                        user.LoggedUsuario = Logged;
                        db.SaveChanges();
                        _usr = user;
                        db.Configuration.LazyLoadingEnabled = false;
                        db.Configuration.ProxyCreationEnabled = false;
                        
                    }
                }
                return _usr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
