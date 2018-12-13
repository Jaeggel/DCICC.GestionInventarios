﻿using System;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Tickets
    {
        public int IdTicket { get; set; }

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public int IdResponsableUsuario { get; set; }

        public string NombreUsuarioResponsable { get; set; }

        public int IdLaboratorio { get; set; }

        public string NombreLaboratorio { get; set; }

        public int IdDetalleActivo { get; set; }

        public string NombreDetalleActivo { get; set; }

        public string EstadoTicket { get; set; }

        public DateTime FechaAperturaTicket { get; set; }

        public DateTime FechaEnProcesoTicket { get; set; }

        public DateTime FechaEnEsperaTicket { get; set; }

        public DateTime FechaResueltoTicket { get; set; }

        public string ComentarioTicket { get; set; }

        public string ComentarioEnCursoTicket { get; set; }

        public string ComentarioEnProcesoTicket { get; set; }

        public string ComentarioResueltoTicket { get; set; }

        public string PrioridadTicket { get; set; }

        public string DescripcionTicket { get; set; }
    }
}
