using DCICC.GestionInventarios.AccesoDatos.UsuariosBD;
using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0)]
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
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarUsuario 
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarUsuario()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Método (GET) para mostrar la vista PerfilUsuario
        /// </summary>
        /// <returns></returns>
        public ActionResult PerfilUsuario()
        {
            if (Session["userInfo"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
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
            string mensajes_Usuarios = string.Empty;
            try
            {
                UsuariosAccDatos objUsuariosAccDatos = new UsuariosAccDatos();
                if(objUsuariosAccDatos.RegistrarUsuario(infoUsuario))
                {
                    TempData["Mensaje"] = "caca";
                    Logs.Info(mensajes_Usuarios);
                }
                else
                {
                    TempData["MensajeError"] = "nope";
                }
                return RedirectToAction("ModificarUsuario", "Usuarios");
            }
            catch(Exception e)
            {
                Logs.Error("Error en el registro de nuevo usuario: "+e.Message);
                return View();
            }
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista PerfilUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PerfilUsuario(Usuarios infoUsuario)
        {
            return RedirectToAction("PerfilUsuario", "Usuarios");
        }
    }
}