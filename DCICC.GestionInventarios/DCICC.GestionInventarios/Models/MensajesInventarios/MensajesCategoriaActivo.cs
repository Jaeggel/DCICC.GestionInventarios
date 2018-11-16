using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesCategoriaActivo
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public CategoriaActivo ObjetoInventarios { get; set; }

        public List<CategoriaActivo> ListaObjetoInventarios { get; set; }
    }
}