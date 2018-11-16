using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;
using System.Text;

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
