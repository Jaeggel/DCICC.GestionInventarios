using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using DCICC.WebServiceInventarios.Configuration;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Roles de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerRolesComp")]
        public MensajesRoles ObtenerRolesComp()
        {
            MensajesRoles msjRoles = new MensajesRoles();
            ConsultasRoles objConsultasRolesBD = new ConsultasRoles();
            msjRoles = objConsultasRolesBD.ObtenerRoles("consultaroles");
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
            List<string> sentenciasGenerales = new List<string>();
            List<string> sentenciasActivos = new List<string>();
            List<string> sentenciasMaqVirtuales = new List<string>();
            List<string> sentenciasTickets = new List<string>();
            List<string> sentenciasReportes = new List<string>();
            SentenciasRoles objSentencias = new SentenciasRoles();
            sentenciasGenerales = objSentencias.ObtenerSentenciasGenerales(infoRol.NombreRol);
            if (infoRol.PermisoActivos)
            {
                sentenciasActivos = objSentencias.ObtenerSentenciasActivos(infoRol.NombreRol);
            }
            if (infoRol.PermisoMaqVirtuales)
            {
                sentenciasMaqVirtuales = objSentencias.ObtenerSentenciasMaqVirtuales(infoRol.NombreRol);
            }
            if (infoRol.PermisoTickets)
            {
                sentenciasTickets= objSentencias.ObtenerSentenciasTickets(infoRol.NombreRol);
            }
            if (infoRol.PermisoReportes)
            {
                sentenciasTickets = objSentencias.ObtenerSentenciasReportes(infoRol.NombreRol);
            }
            MensajesRoles msjRoles = null;
            InsercionesRoles objInsercionesRolesBD = new InsercionesRoles(sentenciasGenerales, sentenciasActivos, sentenciasMaqVirtuales, sentenciasTickets, sentenciasReportes);
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
            List<string> sentenciasGenerales = new List<string>();
            List<string> sentenciasActivos = new List<string>();
            List<string> sentenciasMaqVirtuales = new List<string>();
            List<string> sentenciasTickets = new List<string>();
            List<string> sentenciasReportes = new List<string>();
            List<string> sentenciasRevocacion = new List<string>();
            SentenciasRoles objSentencias = new SentenciasRoles();
            sentenciasRevocacion= objSentencias.ObtenerSentenciasRevocacion(infoRol.NombreRol);
            sentenciasGenerales = objSentencias.ObtenerSentenciasGenerales(infoRol.NombreRol);
            sentenciasGenerales.RemoveAt(0);
            if (infoRol.PermisoActivos)
            {
                sentenciasActivos = objSentencias.ObtenerSentenciasActivos(infoRol.NombreRol);
            }
            if (infoRol.PermisoMaqVirtuales)
            {
                sentenciasMaqVirtuales = objSentencias.ObtenerSentenciasMaqVirtuales(infoRol.NombreRol);
            }
            if (infoRol.PermisoTickets)
            {
                sentenciasTickets = objSentencias.ObtenerSentenciasTickets(infoRol.NombreRol);
            }
            if (infoRol.PermisoReportes)
            {
                sentenciasReportes = objSentencias.ObtenerSentenciasReportes(infoRol.NombreRol);
            }
            MensajesRoles msjRoles = null;
            ActualizacionesRoles objActualizacionesRolesActBD = new ActualizacionesRoles(sentenciasRevocacion,sentenciasGenerales, sentenciasActivos, sentenciasMaqVirtuales, sentenciasTickets, sentenciasReportes);
            msjRoles = objActualizacionesRolesActBD.ActualizacionRol(infoRol);
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
        /// <summary>
        /// Método (POST) para actualizar el estado de un Rol en la base de datos.
        /// </summary>
        /// <param name="infoRol"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoRol")]
        public MensajesRoles ActualizarEstadoRol([FromBody] Roles infoRol)
        {
            MensajesRoles msjRoles = null;
            ActualizacionesRoles objActualizacionesRolesActBD = new ActualizacionesRoles(null,null,null,null,null,null);
            msjRoles = objActualizacionesRolesActBD.ActualizacionEstadoRol(infoRol);
            if (msjRoles.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Rol con ID: {0} realizada exitosamente.", infoRol.IdRol));
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