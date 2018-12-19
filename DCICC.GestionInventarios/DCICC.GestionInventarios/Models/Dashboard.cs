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
    }
}