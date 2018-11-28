using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    public class UsuariosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase Usuarios
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoUsuario
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarUsuario 
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista PerfilUsuario
        /// </summary>
        /// <returns></returns>
        public ActionResult PerfilUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];
                ObtenerUsuarioPorNick();
                return View();
            }
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.RegistrarUsuario(infoUsuario);
                if(msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = "El usuario ha sido registrado exitosamente.";
                    TempData["Mensaje"] = mensajesUsuarios;
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = "No se ha podido registrar el usuario: "+msjUsuarios.MensajeError;
                    TempData["MensajeError"] = mensajesUsuarios;
                }
            }
            catch(Exception e)
            {
                Logs.Error(mensajesUsuarios+": "+e.Message);
                return View();
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarUsuario(Usuarios infoUsuario)
        {
            string mensajes_Usuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario,1);
                if (msjUsuarios.OperacionExitosa)
                {
                    Logs.Info(mensajes_Usuarios);
                }
                else
                {
                    mensajes_Usuarios = "No se ha podido actualizar el usuario: " + msjUsuarios.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Usuarios + ": " + e.Message);
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoUsuario(Usuarios infoUsuario)
        {
            string mensajes_Usuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario, 2);
                if (msjUsuarios.OperacionExitosa)
                {
                    Logs.Info(mensajes_Usuarios);
                }
                else
                {
                    mensajes_Usuarios = "No se ha podido actualizar el usuario: " + msjUsuarios.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Usuarios + ": " + e.Message);
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EliminarUsuario(Usuarios infoUsuario)
        {
            string mensajes_Usuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.EliminarUsuario(infoUsuario);
                if (msjUsuarios.OperacionExitosa)
                {
                    Logs.Info(mensajes_Usuarios);
                }
                else
                {
                    mensajes_Usuarios = "No se ha podido eliminar el usuario: " + msjUsuarios.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Usuarios + ": " + e.Message);
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista PerfilUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarPerfilUsuario(Usuarios infoUsuario)//POR AJAX POR EL ID
        {
            string mensajes_Usuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario,3);
                if (msjUsuarios.OperacionExitosa)
                {
                    Logs.Info(mensajes_Usuarios);
                }
                else
                {
                    mensajes_Usuarios = "No se ha podido actualizar el perfil de usuario: " + msjUsuarios.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajes_Usuarios + ": " + e.Message);
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        /// <summary>
        /// Método para obtener los los usuarios con su rol de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuariosRoles()
        {
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            return Json(objUsuariosRolesAccDatos.ObtenerUsuariosRoles().ListaObjetoInventarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los los usuariso con su rol de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuarioPorNick()
        {
            UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            var datosUsuario = objUsuariosAccDatos.ObtenerUsuariosRoles().ListaObjetoInventarios.Find(x => x.NickUsuario == (string)Session["NickUsuario"]);
            if (datosUsuario != null)
            {
                return Json(datosUsuario.NickUsuario, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}