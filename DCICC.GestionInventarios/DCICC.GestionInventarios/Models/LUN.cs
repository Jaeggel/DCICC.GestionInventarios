using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class LUN
    {
        public int IdLUN { get; set; }

        public int IdStorage { get; set; }

        public string NombreStorage { get; set; }

        public string NombreLUN { get; set; }

        public string CapacidadLun { get; set; }

        public string UnidadLUN { get; set; }

        public string RaidTPLUN { get; set; }

        public string DescripcionLUN { get; set; }

        public bool HabilitadoLUN { get; set; }
    }
}