using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesDashboard
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Dashboard ObjetoInventarios { get; set; }

        public List<Dashboard> ListaObjetoInventarios { get; set; }
    }
}
