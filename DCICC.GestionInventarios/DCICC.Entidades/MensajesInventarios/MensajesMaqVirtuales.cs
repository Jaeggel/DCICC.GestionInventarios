using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesMaqVirtuales
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public MaqVirtuales ObjetoInventarios { get; set; }

        public List<MaqVirtuales> ListaObjetoInventarios { get; set; }
    }
}
