﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class TipoActivo
    {
        public int IdTipoActivo { get; set; }

        public int IdCategoriaActivo { get; set; }

        public string NombreCategoriaActivo { get; set; }

        public string NombreTipoActivo { get; set; }

        public string DescripcionTipoActivo { get; set; }

        public int VidaUtilTipoActivo { get; set; }

        public bool HabilitadoTipoActivo { get; set; }
    }
}