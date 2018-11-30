using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesTickets
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Tickets ObjetoInventarios { get; set; }

        public List<Tickets> ListaObjetoInventarios { get; set; }
    }
}
