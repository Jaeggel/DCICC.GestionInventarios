using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class SistOperativos
    {
        public int IdSistOperativos{ get; set; }

        public string NombreSistOperativos { get; set; }

        public string DescripcionSistOperativos { get; set; }

        public Boolean HabilitadoSistOperativos { get; set; }
    }
}