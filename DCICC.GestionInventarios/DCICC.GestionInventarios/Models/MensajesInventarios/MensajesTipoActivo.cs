using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesTipoActivo
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public TipoActivo ObjetoInventarios { get; set; }

        public List<TipoActivo> ListaObjetoInventarios { get; set; }
    }
}