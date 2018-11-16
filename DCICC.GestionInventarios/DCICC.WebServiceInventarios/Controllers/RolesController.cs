using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Roles")]
    public class RolesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase RolesController
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de roles habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerRolesHab")]
        public MensajesRoles ObtenerRolesHab()
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                ConsultasRoles objConsultasRoles = new ConsultasRoles();
                msjRoles=objConsultasRoles.ObtenerRolesHab();
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los roles: " + e.Message);
            }
            return msjRoles;
        }
    }
}