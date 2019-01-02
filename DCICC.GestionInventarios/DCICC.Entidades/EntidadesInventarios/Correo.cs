using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.Entidades.EntidadesInventarios
{
    public class Correo
    {
        public string EmailReceptor { get; set; }

        public string EmailEmisor { get; set; }

        public string ClaveEmailEmisor { get; set; }

        public int Puerto { get; set; } = 587;

        public string Smtp { get; set; } = "smtp.gmail.com";

        public bool SSL { get; set; } = true;

        public string Asunto { get; set; }

        public string Body { get; set; }
    }
}
