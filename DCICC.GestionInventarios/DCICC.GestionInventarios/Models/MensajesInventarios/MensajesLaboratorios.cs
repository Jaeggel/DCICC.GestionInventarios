using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesLaboratorios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Laboratorios ObjetoInventarios { get; set; }

        public List<Laboratorios> ListaObjetoInventarios { get; set; }
    }
}