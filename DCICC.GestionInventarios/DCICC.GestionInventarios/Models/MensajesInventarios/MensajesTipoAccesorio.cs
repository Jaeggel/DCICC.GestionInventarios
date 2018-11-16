using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesTipoAccesorio
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public TipoAccesorio ObjetoInventarios { get; set; }

        public List<TipoAccesorio> ListaObjetoInventarios { get; set; }
    }
}