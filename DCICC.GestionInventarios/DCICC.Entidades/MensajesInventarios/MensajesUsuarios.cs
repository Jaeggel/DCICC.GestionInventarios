using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesUsuarios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Usuarios ObjetoInventarios { get; set; }

        public List<Usuarios> ListaObjetoInventarios { get; set; }
    }
}
