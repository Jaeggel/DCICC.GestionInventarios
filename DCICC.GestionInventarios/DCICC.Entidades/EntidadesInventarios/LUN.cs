using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class LUN
    {
        public int IdLUN { get; set; }

        public int IdStorage { get; set; }

        public string NombreStorage { get; set; }

        public string NombreLUN { get; set; }

        public string CapacidadLun { get; set; }

        public string RaidTPLUN { get; set; }

        public string DescripcionLUN { get; set; }

        public bool HabilitadoLUN { get; set; }
    }
}
