using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesLaboratorios
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Laboratorios ObjetoInventarios { get; set; }

        public List<Laboratorios> ListaObjetoInventarios { get; set; }
    }
}
