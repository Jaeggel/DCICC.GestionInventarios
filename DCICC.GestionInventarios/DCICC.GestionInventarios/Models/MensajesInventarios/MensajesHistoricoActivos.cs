using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesHistoricoActivos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public HistoricoActivos ObjetoInventarios { get; set; }

        public List<HistoricoActivos> ListaObjetoInventarios { get; set; }
    }
}