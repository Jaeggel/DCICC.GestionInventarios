using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesSistOperativos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public SistOperativos ObjetoInventarios { get; set; }

        public List<SistOperativos> ListaObjetoInventarios { get; set; }
    }
}
