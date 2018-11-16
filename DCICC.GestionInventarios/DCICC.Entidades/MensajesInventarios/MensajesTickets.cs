using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

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
