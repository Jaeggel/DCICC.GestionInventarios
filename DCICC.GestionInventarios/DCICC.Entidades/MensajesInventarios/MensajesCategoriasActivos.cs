using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

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
