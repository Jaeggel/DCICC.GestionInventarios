using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Accesorios
    {
        public int IdAccesorio { get; set; }

        public int IdTipoAccesorio { get; set; }

        public string NombreTipoAccesorio { get; set; }

        public int IdDetalleActivo { get; set; }

        public string NombreDetalleActivo { get; set; }

        public string NombreAccesorio { get; set; }

        public string SerialAccesorio { get; set; }

        public string ModeloAccesorio { get; set; }

        public string DescripcionAccesorio { get; set; }

        public bool HabilitadoAccesorio { get; set; }
    }
}