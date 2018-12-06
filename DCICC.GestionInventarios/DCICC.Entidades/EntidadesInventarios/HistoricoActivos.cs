using System;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class HistoricoActivos
    {
        public int IdHistActivos { get; set; }

        public int IdActivo { get; set; }

        public string NombreActivo { get; set; }

        public int IdAccesorio { get; set; }

        public string NombreAccesorio { get; set; }

        public string NombreTipoActivo { get; set; }

        public string SerialHistActivo { get; set; }

        public string ModeloHistActivo { get; set; }

        public DateTime FechaModifHistActivos { get; set; }
    }
}
