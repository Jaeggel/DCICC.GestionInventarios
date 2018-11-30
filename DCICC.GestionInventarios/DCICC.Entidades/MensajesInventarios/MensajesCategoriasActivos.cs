using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesCategoriasActivos
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public CategoriaActivo ObjetoInventarios { get; set; }

        public List<CategoriaActivo> ListaObjetoInventarios { get; set; }
    }
}
