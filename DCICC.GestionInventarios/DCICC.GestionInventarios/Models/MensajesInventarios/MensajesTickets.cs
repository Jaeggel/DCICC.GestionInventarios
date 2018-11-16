using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesTickets
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Tickets ObjetoInventarios { get; set; }

        public List<Tickets> ListaObjetoInventarios { get; set; }
    }
}