using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesTipoAccesorio
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public TipoAccesorio ObjetoInventarios { get; set; }

        public List<TipoAccesorio> ListaObjetoInventarios { get; set; }
    }
}
