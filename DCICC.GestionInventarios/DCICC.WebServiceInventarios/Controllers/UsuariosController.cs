using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Usuarios")]
    public class UsuariosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase UsuariosController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosComp")]
        public List<Usuarios> ObtenerUsuariosComp()
        {
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                return objConsultasUsuariosBD.ObtenerUsuariosComp();
            }
            catch(Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: "+e.Message);
                return null;
            }
        }
    }
}