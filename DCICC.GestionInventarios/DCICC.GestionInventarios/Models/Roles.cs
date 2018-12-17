﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Roles
    {
        public int IdRol { get; set; }

        public string NombreRol { get; set; }

        public string DescripcionRol { get; set; }

        public bool HabilitadoRol { get; set; }

        public bool PermisoActivos { get; set; }

        public bool PermisoMaqVirtuales { get; set; }

        public bool PermisoTickets { get; set; }

        public bool PermisoReportes { get; set; }

        public bool PermisoAdministracion { get; set; }
    }
}