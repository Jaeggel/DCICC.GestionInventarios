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
    public class TipoActivoController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoTipoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoTipoActivo()
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
        /// Método (GET) para mostrar la vista ModificarTipoActivo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarTipoActivo()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoTipoActivo.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoTipoActivo(TipoActivo infoTipoActivo)
        {
            return RedirectToAction("ModificarTipoActivo", "TipoActivo");
        }
    }
}