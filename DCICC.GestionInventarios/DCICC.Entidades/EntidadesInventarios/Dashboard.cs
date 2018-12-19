using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
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
