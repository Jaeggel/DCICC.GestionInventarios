using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class TipoAccesorio
    {
        public int IdTipoAccesorio { get; set; }

        public string NombreTipoAccesorio { get; set; }

        public string DescripcionTipoAccesorio { get; set; }

        public bool HabilitadoTipoAccesorio { get; set; }
    }
}