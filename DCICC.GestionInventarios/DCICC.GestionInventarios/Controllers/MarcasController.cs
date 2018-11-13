﻿using DCICC.GestionInventarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class MarcasController : Controller
    {
        /// <summary>
        /// Método (GET) para mostrar la vista NuevaMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult NuevaMarca()
        {
            return View();
        }
        /// <summary>
        /// Método (GET) para mostrar la vista ModificarMarca
        /// </summary>
        /// <returns></returns>
        public ActionResult ModificarMarca()
        {
            return View();
        }
        /// <summary>
        /// Método (POST) para recibir los datos provenientes de la vista NuevoMarca.
        /// </summary>
        /// <param name="infoMarcas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaMarca(Marcas infoMarcas)
        {
            return RedirectToAction("ModificarMarca", "Marcas");
        }
    }
}