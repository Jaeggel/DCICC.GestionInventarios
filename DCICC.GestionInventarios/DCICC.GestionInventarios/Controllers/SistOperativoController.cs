﻿using DCICC.GestionInventarios.Configuration;
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
    public class SistOperativoController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevoSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevoSistOperativo()
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
        /// Método (GET) para mostrar la vista ModificarSistOperativo
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarSistOperativo()
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
        /// Método (POST) para recibir los datos provenientes de la vista NuevoSistOperativo.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoSistOperativo(SistOperativos infoSistOperativo)
        {
            return RedirectToAction("ModificarSistOperativo", "SistOperativo");
        }
    }
}