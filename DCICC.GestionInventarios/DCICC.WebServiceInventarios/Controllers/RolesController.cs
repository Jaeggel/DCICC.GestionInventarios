using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Roles")]
    public class RolesController : Controller
    {
        //Instancia para la utilización de LOGS en la clase RolesController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de Roles habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerRolesHab")]
        public MensajesRoles ObtenerRolesHab()
        {
            MensajesRoles msjRoles = new MensajesRoles();
            ConsultasRoles objConsultasRolesBD = new ConsultasRoles();
            msjRoles=objConsultasRolesBD.ObtenerRoles("roleshabilitados");
            if (msjRoles.OperacionExitosa)
            {
                Logs.Info("Consulta de Roles realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjRoles.MensajeError);
            }
            return msjRoles;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost("RegistrarRol")]
        public MensajesRoles RegistrarRol([FromBody] Roles infoRol)
        {
            MensajesRoles msjRoles = null;
            InsercionesRoles objInsercionesRolesBD = new InsercionesRoles();
            msjRoles = objInsercionesRolesBD.RegistroRol(infoRol);
            if (msjRoles.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Rol \"{0}\" realizado exitosamente.",infoRol.NombreRol));
            }
            else
            {
                Logs.Error(msjRoles.MensajeError);
            }
            return msjRoles;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost("ActualizarRol")]
        public MensajesRoles ActualizarRol([FromBody] Roles infoRol)
        {
            MensajesRoles msjRoles = null;
            if (msjRoles.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Rol con ID: {0} realizada exitosamente.",infoRol.IdRol));
            }
            else
            {
                Logs.Error(msjRoles.MensajeError);
            }
            return msjRoles;
        }
        #endregion
    }
}