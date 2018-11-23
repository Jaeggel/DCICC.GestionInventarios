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
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                msjUsuarios=objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");//corregir
            }
            catch(Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: "+e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                msjUsuarios= objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                msjUsuarios=objConsultasUsuariosBD.ObtenerUsuarios("usuariosroles");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                InsercionesUsuarios objInsercionesUsuariosBD = new InsercionesUsuarios();
                msjUsuarios=objInsercionesUsuariosBD.RegistroUsuario(infoUsuario);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
                msjUsuarios = objActualizacionesUsuariosBD.ActualizacionUsuario(infoUsuario);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                ActualizacionesUsuarios objActualizacionesUsuariosBD = new ActualizacionesUsuarios();
                msjUsuarios = objActualizacionesUsuariosBD.ActualizacionPerfilUsuario(infoUsuario);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
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
            try
            {
                EliminacionesUsuarios objEliminacionesUsuariosBD = new EliminacionesUsuarios();
                msjUsuarios = objEliminacionesUsuariosBD.EliminacionUsuario(infoUsuario);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo actualizar el usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
    }
}