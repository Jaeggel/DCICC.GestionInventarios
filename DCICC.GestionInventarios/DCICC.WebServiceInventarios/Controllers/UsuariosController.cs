using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ActualizacionesBD;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.EliminacionesBD;
using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
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
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para obtener una lista de todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosComp")]
        public MensajesUsuarios ObtenerUsuariosComp()
        {
            MensajesUsuarios msjUsuarios = null;
            ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
            msjUsuarios=objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");//corregir
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
        /// Método (GET) para obtener una lista de todos los usuarios habilitados de la base de datos.
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
        /// Método (GET) para obtener una lista de la función usuariosroles de la base de datos.
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
        /// Método (POST) para registrar un nuevo usuario en la base de datos.
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
                Logs.Info("Registro de Usuario realizado exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar un usuario en la base de datos.
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
                Logs.Info("Actualización de Usuario realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar un usuario en la base de datos.
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
                Logs.Info("Actualización de estado de Usuario realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para actualizar el perfil de un usuario en la base de datos.
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
                Logs.Info("Actualización de perfil de usuario realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método (POST) para eliminar un usuario en la base de datos.
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
                Logs.Info("Eliminación de Usuario realizada exitosamente.");
            }
            else
            {
                Logs.Error(msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
    }
}