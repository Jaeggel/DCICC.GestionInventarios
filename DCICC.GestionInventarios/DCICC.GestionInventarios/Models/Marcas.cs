using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Marcas
    {
        public int IdMarca { get; set; }

        public string NombreMarca { get; set; }

        public string DescripcionMarca { get; set; }

        public bool HabilitadoMarca { get; set; }
    }
}