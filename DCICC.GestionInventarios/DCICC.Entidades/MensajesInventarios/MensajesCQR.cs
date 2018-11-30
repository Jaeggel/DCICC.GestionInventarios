using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesCQR
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public CQR ObjetoInventarios { get; set; }

        public List<CQR> ListaObjetoInventarios { get; set; }
    }
}
