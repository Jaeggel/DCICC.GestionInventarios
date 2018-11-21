using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class HistoricoActivos
    {
        public int IdHistActivos { get; set; }

        public int IdDetActivo { get; set; }

        public DateTime FechaModifHistActivos { get; set; }
    }
}