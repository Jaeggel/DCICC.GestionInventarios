using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Storage
    {
        public int IdStorage { get; set; }

        public string NombreStorage { get; set; }

        public int CapacidadStorage { get; set; }

        public string DescripcionStorage { get; set; }

        public string HabilitadoStorage { get; set; }
    }
}