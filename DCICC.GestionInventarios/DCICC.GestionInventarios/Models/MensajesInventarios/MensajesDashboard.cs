using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesDashboard
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Dashboard ObjetoInventarios { get; set; }

        public List<Dashboard> ListaObjetoInventarios { get; set; }
    }
}