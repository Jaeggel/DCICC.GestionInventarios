﻿using System;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class MaqVirtuales
    {
        public int IdMaqVirtuales { get; set; }

        public string NombreSistOperativos { get; set; }

        public int IdSistOperativos{ get; set; }

        public int IdLUN { get; set; }

        public DateTime FechaCreacionMaqVirtuales { get; set; }

        public DateTime FechaExpiracionMaqVirtuales { get; set; }

        public string NombreLUN { get; set; }

        public string UsuarioMaqVirtuales { get; set; }

        public string NombreMaqVirtuales { get; set; }

        public string PropositoMaqVirtuales { get; set; }

        public string DireccionIPMaqVirtuales { get; set; }

        public string UnidadMaqVirtuales { get; set; }

        public int SizeMaqVirtuales { get; set; }

        public string DiscoMaqVirtuales { get; set; }

        public int RamMaqVirtuales { get; set; }

        public string DescripcionMaqVirtuales { get; set; }

        public bool HabilitadoMaqVirtuales { get; set; }
    }
}
