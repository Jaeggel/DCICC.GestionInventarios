using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesDetalleActivo
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public DetalleActivos ObjetoInventarios { get; set; }

        public List<DetalleActivos> ListaObjetoInventarios { get; set; }
    }
}
