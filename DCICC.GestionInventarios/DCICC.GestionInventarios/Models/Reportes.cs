﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Reportes
    {
        public string TituloCarrera { get; set; }

        public string TituloSistema { get; set; }

        public string TituloReporte { get; set; }

        public string TituloSedeCampus { get; set; }

        public HttpPostedFileBase Imagen { get; set; }

        public bool ImagenJPG { get; set; }

        public bool ImagenPNG { get; set; }
    }
}