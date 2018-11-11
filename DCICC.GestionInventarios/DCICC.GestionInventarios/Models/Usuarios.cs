﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public string NombreRol { get; set; }

        public string NombresUsuario { get; set; }

        public string NickUsuario { get; set; }

        public string PasswordUsuario { get; set; }

        public string CorreoUsuario { get; set; }

        public string TelefonoUsuario { get; set; }

        public string TelefonoCelUsuario { get; set; }

        public string DireccionUsuario { get; set; }

        public Boolean HabilitadoUsuario { get; set; }

    }
}