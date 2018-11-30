using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesActivos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Activos ObjetoInventarios { get; set; }

        public List<Activos> ListaObjetoInventarios { get; set; }
    }
}
