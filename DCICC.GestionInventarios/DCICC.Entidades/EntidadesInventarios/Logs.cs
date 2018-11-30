using System;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Logs
    {
        public int IdLogs { get; set; }

        public string IdUsuario { get; set; }

        public DateTime FechaLogs { get; set; }

        public string OperacionLogs { get; set; }

        public string ValorAnteriorLogs { get; set; }

        public string ValorActualLogs { get; set; }

        public string TablaLogs { get; set; }

        public string IpLogs { get; set; }

    }
}
