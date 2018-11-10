using System;
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

        public Boolean Habilitado { get; set; }
    }
}