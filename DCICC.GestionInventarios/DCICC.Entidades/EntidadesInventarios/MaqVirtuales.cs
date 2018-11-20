using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class MaqVirtuales
    {
        public int IdMaqVirtuales { get; set; }

        public string NombreSistOperativos { get; set; }

        public int IdSistOperativos{ get; set; }

        public string UsuarioMaqVirtuales { get; set; }

        public string NombreMaqVirtuales { get; set; }

        public string PropositoMaqVirtuales { get; set; }

        public string DireccionIPMaqVirtuales { get; set; }

        public int DiscoMaqVirtuales { get; set; }

        public int RamMaqVirtuales { get; set; }

        public string DescripcionMaqVirtuales { get; set; }

        public Boolean HabilitadoMaqVirtuales { get; set; }
    }
}
