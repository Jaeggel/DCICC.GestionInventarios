using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesSistOperativos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public SistOperativos ObjetoInventarios { get; set; }

        public List<SistOperativos> ListaObjetoInventarios { get; set; }
    }
}