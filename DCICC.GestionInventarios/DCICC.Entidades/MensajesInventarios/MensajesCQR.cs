using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesCQR
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public CQR ObjetoInventarios { get; set; }

        public List<CQR> ListaObjetoInventarios { get; set; }
    }
}
