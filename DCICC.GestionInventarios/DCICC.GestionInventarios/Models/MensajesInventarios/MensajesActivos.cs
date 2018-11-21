using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesActivos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Activos ObjetoInventarios { get; set; }

        public List<Activos> ListaObjetoInventarios { get; set; }
    }
}