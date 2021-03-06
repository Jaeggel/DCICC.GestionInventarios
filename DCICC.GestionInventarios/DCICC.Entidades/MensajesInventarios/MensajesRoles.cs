﻿using DCICC.Entidades.EntidadesInventarios;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesRoles
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Roles ObjetoInventarios { get; set; }

        public List<Roles> ListaObjetoInventarios { get; set; }
    }
}
