using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesAccesorios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Accesorios ObjetoInventarios { get; set; }

        public List<Accesorios> ListaObjetoInventarios { get; set; }
    }
}
