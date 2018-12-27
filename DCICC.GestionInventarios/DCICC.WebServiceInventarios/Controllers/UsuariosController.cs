using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.EliminacionesBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCICC.WebServiceInventarios.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("Usuarios")]
    public class UsuariosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase UsuariosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Consultas
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosComp")]
        public MensajesUsuarios ObtenerUsuariosComp()
        {
            MensajesUsuarios msjUsuarios = null;
            ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
            msjUsuarios=objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info("Consulta de Usuarios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Usuarios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosHab")]
        public MensajesUsuarios ObtenerUsuariosHab()
        {
            MensajesUsuarios msjUsuarios = null;
            ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
            msjUsuarios= objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados");
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info("Consulta de Usuarios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los Usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosRoles")]
        public MensajesUsuarios ObtenerUsuariosRoles()
        {
            MensajesUsuarios msjUsuarios = null;
            ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
            msjUsuarios=objConsultasUsuariosBD.ObtenerUsuarios("usuariosroles");
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info("Consulta de Usuarios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (GET) para obtener una lista de los Usuarios con permisos en tickets de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosRespTickets")]
        public MensajesUsuarios ObtenerUsuariosRespTickets()
        {
            MensajesUsuarios msjUsuarios = null;
            ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
            msjUsuarios = objConsultasUsuariosBD.ObtenerUsuarios("responsablestickets");
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info("Consulta de Usuarios realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método (POST) para registrar un nuevo Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("RegistrarUsuario")]
        public MensajesUsuarios RegistrarUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            InsercionesUsuarios objInsercionesUsuariosBD = new InsercionesUsuarios();
            msjUsuarios=objInsercionesUsuariosBD.RegistroUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Registro de Usuario \"{0}\" realizado exitosamente.",infoUsuario.NickUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método (POST) para actualizar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("ActualizarUsuario")]
        public MensajesUsuarios ActualizarUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
            msjUsuarios = objActualizacionesUsuariosBD.ActualizacionUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de Usuario con ID: {0} realizada exitosamente.",infoUsuario.IdUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("ActualizarEstadoUsuario")]
        public MensajesUsuarios ActualizarEstadoUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
            msjUsuarios = objActualizacionesUsuariosBD.ActualizacionEstadoUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de estado de Usuario con ID: {0} realizada exitosamente.", infoUsuario.IdUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar el perfil de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("ActualizarPerfilUsuario")]
        public MensajesUsuarios ActualizarPerfilUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
            msjUsuarios = objActualizacionesUsuariosBD.ActualizacionPerfilUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de perfil de Usuario con ID: {0} realizada exitosamente.", infoUsuario.IdUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar el password de un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("ActualizarPasswordUsuario")]
        public MensajesUsuarios ActualizarPasswordUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
            msjUsuarios = objActualizacionesUsuariosBD.ActualizacionPasswordUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Actualización de password de Usuario con ID: {0} realizada exitosamente.", infoUsuario.IdUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        #endregion
        #region Eliminaciones
        /// <summary>
        /// Método (POST) para eliminar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("EliminarUsuario")]
        public MensajesUsuarios EliminarUsuario([FromBody] Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = null;
            EliminacionesUsuarios objEliminacionesUsuariosBD = new EliminacionesUsuarios();
            msjUsuarios = objEliminacionesUsuariosBD.EliminacionUsuario(infoUsuario);
            if (msjUsuarios.OperacionExitosa)
            {
                Logs.Info(string.Format("Eliminación de Usuario con ID: {0} realizada exitosamente.", infoUsuario.IdUsuario));
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        #endregion
    }
}