using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesAccesorios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Accesorios ObjetoInventarios { get; set; }

        public List<Accesorios> ListaObjetoInventarios { get; set; }
    }
}