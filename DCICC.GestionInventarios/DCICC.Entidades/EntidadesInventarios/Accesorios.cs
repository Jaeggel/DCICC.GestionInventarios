﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Accesorios
    {
        public int IdAccesorio { get; set; }

        public int IdTipoAccesorio { get; set; }

        public string NombreTipoAccesorio { get; set; }

        public int IdDetalleActivo { get; set; }

        public string NombreAccesorio { get; set; }

        public string SerialAccesorio { get; set; }

        public string ModeloAccesorio { get; set; }

        public string DescripcionAccesorio { get; set; }

        public Boolean HabilitadoAccesorio { get; set; }
    }
}
