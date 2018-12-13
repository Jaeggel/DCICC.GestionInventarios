using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesStorage
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Storage ObjetoInventarios { get; set; }

        public List<Storage> ListaObjetoInventarios { get; set; }
    }
}