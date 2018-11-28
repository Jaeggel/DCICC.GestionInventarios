using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class CategoriaActivo
    {
        public int IdCategoriaActivo { get; set; }

        public string NombreCategoriaActivo { get; set; }

        public string DescripcionCategoriaActivo { get; set; }

        public bool HabilitadoCategoriaActivo { get; set; }
    }
}