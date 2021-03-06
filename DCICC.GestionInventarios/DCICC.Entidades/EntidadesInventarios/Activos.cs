﻿using System;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Activos
    {
        public int IdActivo { get; set; }

        //Categoría de Activo
        public string NombreCategoriaActivo { get; set; }

        //Tipo de Activo
        public int IdTipoActivo { get; set; }

        public string NombreTipoActivo { get; set; }

        public int VidaUtilTipoActivo { get; set; }

        public DateTime VidaFinalTipoActivo { get; set; }

        //Laboratorio
        public int IdLaboratorio { get; set; }

        public string NombreLaboratorio { get; set; }

        //Marca
        public int IdMarca { get; set; }

        public string NombreMarca { get; set; }

        public string IdCQR { get; set; }

        public bool ImpresoCQR { get; set; }

        public string ResponsableActivo { get; set; }

        public string NombreActivo { get; set; }

        public string EstadoActivo { get; set; }

        public string ModeloActivo { get; set; }

        public string SerialActivo { get; set; }

        public DateTime FechaIngresoActivo { get; set; }

        public string CodigoUpsActivo { get; set; }

        public int CantidadActivo { get; set; } = 1;

        public string DescripcionActivo { get; set; }

        public string ExpressServiceCodeActivo { get; set; }

        public string ProductNameActivo { get; set; }

        public string CapacidadActivo { get; set; }

        public string VelocidadTransfActivo { get; set; }

        public string CtActivo { get; set; }

        public string HpePartNumberActivo { get; set; }

        public string CodBarras1Activo { get; set; }

        public string CodBarras2Activo { get; set; }

        public int NumPuertosActivo { get; set; } = 0;

        public string IosVersionActivo { get; set; }

        public string FechaManufacturaActivo { get; set; }

        public byte[] BytesCQR { get; set; }

        public bool DeBaja { get; set; } = false;
    }
}
