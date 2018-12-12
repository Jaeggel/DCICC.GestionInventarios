using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Models
{
    public class LUN
    {
        public int IdBDLUN { get; set; }

        public string IdLUN { get; set; }

        public int SizeLun { get; set; }//?

        public string NombreLUN { get; set; }

        public string NombreStorage { get; set; }//COMBO

        public string RaidLUN { get; set; }

        public string TipoConexionLUN { get; set; }//COMBO

        public string DescripcionLUN { get; set; }

        public bool HabilitadoLUN { get; set; }
    }
}