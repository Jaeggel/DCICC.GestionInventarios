﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCICC.GestionInventarios.Controllers
{
    public class MarcasController : Controller
    {
        // GET: Marcas
        public ActionResult NuevaMarca()
        {
            return View();
        }

        // GET: Modificar Marcas
        public ActionResult ModificarMarca()
        {
            return View();
        }
    }
}