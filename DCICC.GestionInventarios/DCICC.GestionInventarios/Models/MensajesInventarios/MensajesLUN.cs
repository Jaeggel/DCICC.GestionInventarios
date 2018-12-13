using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesLUN
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public LUN ObjetoInventarios { get; set; }

        public List<LUN> ListaObjetoInventarios { get; set; }
    }
}