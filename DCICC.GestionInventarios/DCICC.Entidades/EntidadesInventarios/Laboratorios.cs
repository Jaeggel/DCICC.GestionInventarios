﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Laboratorios
    {
        public int IdLaboratorio { get; set; }

        public string NombreLaboratorio { get; set; }

        public string UbicacionLaboratorio { get; set; }

        public string DescripcionLaboratorio { get; set; }

        public Boolean HabilitadoLaboratorio { get; set; }
    }
}