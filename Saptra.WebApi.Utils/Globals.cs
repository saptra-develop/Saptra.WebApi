using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Utils
{
    public static class Globals
    {
        //CATALOGO TIPO ESTATUS
        public static string TES_BORRADO_LOGICO = "Borrado Logico";
        public static string TES_PLAN_SEMANAL = "Plan Semanal";
        //CATALOGO ESTATUS
        public static string EST_ACTIVO = "Activo";
        public static string EST_INACTIVO = "Inactivo";
        public static string EST_VALIDADO= "Validado";
        //CATALOGO TIPO_FIGURA
        public static string[] CAT_TIPO_FIGURA = { "Promotor", "Tecnico docente", "Formador" };
    }
}
