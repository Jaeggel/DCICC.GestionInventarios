using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesLUN
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public LUN ObjetoInventarios { get; set; }

        public List<LUN> ListaObjetoInventarios { get; set; }
    }
}
