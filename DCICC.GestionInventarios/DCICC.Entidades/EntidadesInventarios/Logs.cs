using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Logs
    {
        public int IdLogs { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaLogs { get; set; }

        public string OperacionLogs { get; set; }

        public string ValorAnteriorLogs { get; set; }

        public string ValorActualLogs { get; set; }

        public string TablaLogs { get; set; }
    }
}
