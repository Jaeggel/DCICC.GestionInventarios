using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos.ConsultasBD;
using DCICC.AccesoDatos.InsercionesBD;
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
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                return objConsultasUsuariosBD.ObtenerUsuarios("consultausuarios");
            }
            catch(Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: "+e.Message);
                return null;
            }
        }
        /// <summary>
        /// Método (GET) para obtener una lista de todos los usuarios habilitados de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosHab")]
        public List<Usuarios> ObtenerUsuariosHab()
        {
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                return objConsultasUsuariosBD.ObtenerUsuarios("usuarioshabilitados");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Método (GET) para obtener una lista de la función usuariosroles de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerUsuariosRoles")]
        public List<Usuarios> ObtenerUsuariosRoles()
        {
            try
            {
                ConsultasUsuarios objConsultasUsuariosBD = new ConsultasUsuarios();
                return objConsultasUsuariosBD.ObtenerUsuariosRoles();
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo obtener la lista de los usuarios: " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Método (POST) para registrar un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost("RegistrarUsuario")]
        public bool RegistrarUsuario([FromBody] Usuarios infoUsuario)
        {
            try
            {
                InsercionesUsuarios objInsercionesUsuariosBD = new InsercionesUsuarios();
                return objInsercionesUsuariosBD.RegistroUsuario(infoUsuario);
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo registrar el usuario: " + e.Message);
                return false;
            }
        }
    }
}