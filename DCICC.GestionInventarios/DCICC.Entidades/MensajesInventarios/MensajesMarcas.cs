using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesMarcas
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Marcas ObjetoInventarios { get; set; }

        public List<Marcas> ListaObjetoInventarios { get; set; }
    }
}
