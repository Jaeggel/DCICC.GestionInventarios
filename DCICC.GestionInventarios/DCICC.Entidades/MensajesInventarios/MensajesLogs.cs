﻿using DCICC.Entidades.EntidadesInventarios;
using System;
using System.Collections.Generic;

namespace DCICC.Entidades.MensajesInventarios
{
    public class MensajesLogs
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public long ValorLong { get; set; }

        public Logs ObjetoInventarios { get; set; }

        public List<Logs> ListaObjetoInventarios { get; set; }
    }
}
