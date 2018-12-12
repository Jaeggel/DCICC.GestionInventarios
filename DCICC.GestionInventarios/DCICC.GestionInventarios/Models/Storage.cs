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

        public string NickStorage { get; set; }

        public string CapacidadStorage { get; set; }

        public int SizeStorage { get; set; }

        public string UnidadStorage { get; set; }

        public string DescripcionStorage { get; set; }

        public bool HabilitadoStorage { get; set; }
    }
}