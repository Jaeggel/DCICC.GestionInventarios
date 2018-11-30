using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

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
