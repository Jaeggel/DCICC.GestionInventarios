using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Dashboard
    {
        public long SesionCont { get; set; }

        public long ActivosOperativosCont { get; set; }

        public long ActivosNoOperativosCont { get; set; }

        public long ActivosDeBajaCont { get; set; }

        public long UsuariosHabilitadosCont { get; set; }

        public long TicketsAbiertosCont { get; set; }

        public long TicketsEnProcesoCont { get; set; }

        public long TicketsEnEsperaCont { get; set; }

        public long TicketsResueltosCont { get; set; }

        public string NombreTipoActivo { get; set; }

        public long TipoActivoCont { get; set; }
    }
}