using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class Logs
    {
        public int IdLogs { get; set; }

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public DateTime FechaLogs{ get; set; }

        public string OperacionLogs { get; set; }

        public string ValorAnteriorLogs { get; set; }

        public string ValorActualLogs { get; set; }

        public string TablaLogs { get; set; }

        public string IpLogs { get; set; }
    }
}