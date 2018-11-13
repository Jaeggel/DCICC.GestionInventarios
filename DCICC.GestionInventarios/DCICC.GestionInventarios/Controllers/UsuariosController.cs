using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class UsuariosController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoUsuario
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoUsuario()
        {
            return View();
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarUsuario 
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarUsuario()
        {
            return View();
        }
        /// <summary>
        /// Método (GET) para mostrar la vista PerfilUsuario
        /// </summary>
        /// <returns></returns>
        public ActionResult PerfilUsuario()
        {
            return View();
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoUsuario.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoUsuario(Usuarios infoUsuario)
        {
            return RedirectToAction("ModificarUsuario", "Usuarios");
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