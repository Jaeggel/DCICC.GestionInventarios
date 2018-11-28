using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Laboratorios
    {
        public int IdLaboratorio { get; set; }

        public string NombreLaboratorio { get; set; }

        public string UbicacionLaboratorio { get; set; }

        public string DescripcionLaboratorio{ get; set; }

        public bool HabilitadoLaboratorio { get; set; }

    }
}