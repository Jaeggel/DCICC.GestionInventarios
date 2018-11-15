using DCICC.GestionInventarios.Configuration;
using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    [SessionExpireFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class TipoAccesorioController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoAccesorio()
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
        /// Método (GET) para mostrar la vista ModificarTipoAccesorio
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoAccesorio()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoTipoAccesorio.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            return RedirectToAction("ModificarTipoAccesorio", "TipoAccesorio");
        }
    }
}