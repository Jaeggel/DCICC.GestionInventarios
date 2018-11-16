using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesUsuarios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Usuarios ObjetoInventarios { get; set; }

        public List<Usuarios> ListaObjetoInventarios { get; set; }
    }
}