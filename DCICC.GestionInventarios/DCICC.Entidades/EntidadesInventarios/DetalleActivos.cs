using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class DetalleActivos
    {
        public int IdDetActivo { get; set; }

        public int IdCategoriaDetActivo { get; set; }

        public int IdTipoDetActivo { get; set; }

        public int IdLaboratorio { get; set; }

        public int IdMarca { get; set; }

        public int IdCQR { get; set; }

        public string NombreDetActivo { get; set; }

        public string ModeloDetActivo { get; set; }

        public string SerialDetActivo { get; set; }

        public string FechaIngresoDetActivo { get; set; }

        public string CodigoUpsDetActivo { get; set; }

        public int CantidadDetActivo { get; set; }

        public string DescripcionDetActivo { get; set; }

        public string EstadoDetActivo { get; set; }

        public string ExpressServiceCodeDetActivo { get; set; }

        public string ProductNameDetActivo { get; set; }

        public string CapacidadDetActivo { get; set; }

        public string VelocidadTransfDetActivo { get; set; }

        public string CtDetActivo { get; set; }

        public string HpePartNumberDetActivo { get; set; }

        public string CodBarras1DetActivo { get; set; }

        public string CodBarras2DetActivo { get; set; }

        public int NumPuertosDetActivo { get; set; }

        public string IosVersionDetActivo { get; set; }

        public string FechaManufacturaDetActivo { get; set; }
    }
}
