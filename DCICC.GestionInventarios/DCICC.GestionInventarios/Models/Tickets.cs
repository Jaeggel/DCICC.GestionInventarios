using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
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

        public DateTime FechaSolucionTicket { get; set; }

        public string PrioridadTicket { get; set; }

        public string ComentarioTicket { get; set; }

        public string DescripcionTicket { get; set; }
    }
}