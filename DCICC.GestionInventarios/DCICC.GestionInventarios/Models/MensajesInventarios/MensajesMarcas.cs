﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models.MensajesInventarios
{
    public class MensajesMarcas
    {
        public bool OperacionExitosa { get; set; }

        public string MensajeError { get; set; }

        public Marcas ObjetoInventarios { get; set; }

        public List<Marcas> ListaObjetoInventarios { get; set; }
    }
}