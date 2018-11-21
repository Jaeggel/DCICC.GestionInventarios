using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesHistoricoActivos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public HistoricoActivos ObjetoInventarios { get; set; }

        public List<HistoricoActivos> ListaObjetoInventarios { get; set; }
    }
}
