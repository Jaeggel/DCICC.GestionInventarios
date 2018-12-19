using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesLogs
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public long ValorLong { get; set; }

        public Logs ObjetoInventarios { get; set; }

        public List<Logs> ListaObjetoInventarios { get; set; }
    }
}