using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class MaqVirtuales
    {
        public string NombreSistOperativo{ get; set; }

        public string NombreMaqVirtuales{ get; set; }

        public string PropositoMaqVirtuales { get; set; }

        public string DireccionIPMaqVirtuales { get; set; }

        public int DiscoMaqVirtuales { get; set; }

        public int RamMaqVirtuales { get; set; }

        public string DescripcionMaqVirtuales{ get; set; }

        public Boolean HabilitadoMaqVirtuales { get; set; }
    }
}