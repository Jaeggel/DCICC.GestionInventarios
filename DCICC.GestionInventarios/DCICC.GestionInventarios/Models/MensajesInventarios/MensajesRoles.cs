using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesRoles
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Roles ObjetoInventarios { get; set; }

        public List<Roles> ListaObjetoInventarios { get; set; }
    }
}