using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

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
