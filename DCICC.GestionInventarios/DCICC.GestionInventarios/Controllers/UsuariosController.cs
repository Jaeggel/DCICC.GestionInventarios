using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class UsuariosController : Controller
    {
        //Instancia para la utilización de LOGS en la clase UsuariosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Vistas (GET)
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoUsuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NuevoUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
        [HttpGet]
        public ActionResult ModificarUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
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
        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            if ((string)Session["NickUsuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.NombreUsuario = Regex.Replace((string)Session["NombresUsuario"], @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                ViewBag.UsuarioLogin = (string)Session["NickUsuario"];
                ViewBag.Correo = (string)Session["CorreoUsuario"];
                ViewBag.Menu = (string)Session["PerfilUsuario"];        
                return View();
            }
        }
        #endregion
        #region Registros (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoUsuarioPOST(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.RegistrarUsuario(infoUsuario);
                if(msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El usuario \"{0}\" ha sido registrado exitosamente.",infoUsuario.NickUsuario);
                    TempData["Mensaje"] = mensajesUsuarios;
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido registrar el usuario \"{0}\": {1}",infoUsuario.NickUsuario,msjUsuarios.MensajeError);
                    TempData["MensajeError"] = mensajesUsuarios;
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
                return View();
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        #endregion
        #region Actualizaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario,1);
                if (msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El usuario con ID: {0} ha sido modificado correctamente.",infoUsuario.IdUsuario);
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido actualizar el usuario con ID: {0}: {1}",infoUsuario.IdUsuario,msjUsuarios.MensajeError);
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
            }
            return Json(msjUsuarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista PerfilUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarPerfilUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario, 3);
                if (msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El perfil de usuario con ID: {0} ha sido modificado correctamente.", infoUsuario.IdUsuario);
                    TempData["Mensaje"]= string.Format("El usuario \"{0}\" ha sido modificado correctamente.", (string)Session["NickUsuario"]);
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido actualizar el perfil de usuario con ID: {0}: {1}", infoUsuario.IdUsuario, msjUsuarios.MensajeError);
                    TempData["MensajeError"] = mensajesUsuarios;
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
            }
            return Json(msjUsuarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarEstadoUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario, 2);
                if (msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El usuario con ID: {0} ha sido modificado correctamente.", infoUsuario.IdUsuario);
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido actualizar el usuario con ID: {0}: {1}", infoUsuario.IdUsuario, msjUsuarios.MensajeError);
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
            }
            return Json(msjUsuarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista PerfilUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarPasswordUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.ActualizarUsuario(infoUsuario, 4);
                if (msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El usuario con ID: {0} ha sido modificado correctamente.", infoUsuario.IdUsuario);
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido actualizar el usuario con ID: {0}: {1}", infoUsuario.IdUsuario, msjUsuarios.MensajeError);
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
            }
            return Json(msjUsuarios, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Eliminaciones (POST)
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista ModificarUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EliminarUsuario(Usuarios infoUsuario)
        {
            string mensajesUsuarios = string.Empty;
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
                msjUsuarios = objUsuariosAccDatos.EliminarUsuario(infoUsuario);
                if (msjUsuarios.OperacionExitosa)
                {
                    mensajesUsuarios = string.Format("El usuario con ID: {0} ha sido eliminado correctamente.", infoUsuario.IdUsuario);
                    Logs.Info(mensajesUsuarios);
                }
                else
                {
                    mensajesUsuarios = string.Format("No se ha podido eliminar el usuario con ID: {0}: {1}", infoUsuario.IdUsuario, msjUsuarios.MensajeError);
                    Logs.Error(mensajesUsuarios);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("{0}: {1}", mensajesUsuarios, e.Message));
            }
            return RedirectToAction("ModificarUsuario", "Usuarios");
        }
        #endregion
        #region Consultas (JSON)
        /// <summary>
        /// Método para obtener todos los Usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuariosRoles()
        {
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            return Json(objUsuariosRolesAccDatos.ObtenerUsuarios("Roles"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener todos los Usuarios habilitados de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuariosHab()
        {
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            return Json(objUsuariosRolesAccDatos.ObtenerUsuarios("Hab"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener los Usuarios con permisos para tickets.
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuariosRespTickets()
        {
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            return Json(objUsuariosRolesAccDatos.ObtenerUsuarios("RespTickets"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener todos los Usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerNicksUsuarios()
        {
            List<string> lstNombresUsuarios = new List<string>();
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            var lst= objUsuariosRolesAccDatos.ObtenerUsuarios("Roles").ListaObjetoInventarios;
            foreach (var item in lst)
            {
                lstNombresUsuarios.Add(item.NickUsuario);
            }
            return Json(lstNombresUsuarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener todos los ids, nombres y roles de los Usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuarios()
        {
            List<Usuarios> lstNombresUsuarios = new List<Usuarios>();
            UsuariosAccDatos objUsuariosRolesAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            var lst = objUsuariosRolesAccDatos.ObtenerUsuarios("Roles").ListaObjetoInventarios;
            foreach (var item in lst)
            {
                Usuarios objUsuario = new Usuarios()
                {
                    NombresUsuario = item.NombresUsuario,
                    NombreRol = item.NombreRol,
                    IdUsuario = item.IdUsuario
                };
                lstNombresUsuarios.Add(objUsuario);
            }
            return Json(lstNombresUsuarios, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para obtener el Usuario Actual del Sistema de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerUsuarioPorNick()
        {
            UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos((string)Session["NickUsuario"]);
            var datosUsuario = objUsuariosAccDatos.ObtenerUsuarios("Roles").ListaObjetoInventarios.Find(x => x.NickUsuario == (string)Session["NickUsuario"]);
            if (datosUsuario != null)
            {
                return Json(datosUsuario, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Método para obtener el rol del Usuario Actual del Sistema de la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerRolActual()
        {
            return Json((string)Session["PerfilUsuario"], JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}