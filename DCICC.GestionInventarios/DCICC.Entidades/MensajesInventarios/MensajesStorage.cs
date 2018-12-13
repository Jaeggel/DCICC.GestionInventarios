using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesStorage
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Storage ObjetoInventarios { get; set; }

        public List<Storage> ListaObjetoInventarios { get; set; }
    }
}
