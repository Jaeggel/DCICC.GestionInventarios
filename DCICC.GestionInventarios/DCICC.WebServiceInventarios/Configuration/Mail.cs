using DCICC.Entidades.EntidadesInventarios;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DCICC.WebServiceInventarios.Configuration
{
    public class Mail
    {
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ConfigurationBuilder configuration_Builder = new ConfigurationBuilder();
        string path_ArchivoConfig = string.Empty;
        /// <summary>
        /// Constructor para llamar a los métodos que se encargan de obtener el path del HTML. 
        /// </summary>
        public Mail()
        {
            configuration_Builder = new ConfigurationBuilder();
            path_ArchivoConfig = Path.Combine(Directory.GetCurrentDirectory(), "Configuration\\InfoTicket.html");
        }

        /// <summary>
        /// Método para enviar el Correo Electrónico.
        /// </summary>
        /// <param name="infoCorreo"></param>
        /// <returns></returns>
        public void SendMail(Correo infoCorreo)
        {            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(infoCorreo.EmailEmisor);
            mail.To.Add(infoCorreo.EmailReceptor);
            mail.Subject = infoCorreo.Asunto;
            mail.Body = infoCorreo.Body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = infoCorreo.Puerto;
            SmtpServer.Host = infoCorreo.Smtp;
            SmtpServer.EnableSsl = infoCorreo.SSL;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(infoCorreo.EmailEmisor, infoCorreo.ClaveEmailEmisor);
            SmtpServer.Send(mail);
        }
        /// <summary>
        /// Método para dar formato al Email mediante un archivo HTML (InfoTicket.html)
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public string FormatBody(Tickets infoTicket)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path_ArchivoConfig))
            {
                body = reader.ReadToEnd();
            }
            string tituloTic = string.Empty;
            string valorTic = string.Empty;
            if(infoTicket.NombreLaboratorio!=null)
            {
                tituloTic = "Laboratorio";
                valorTic = infoTicket.NombreLaboratorio;
            }
            else if(infoTicket.NombreDetalleActivo!=null)
            {
                tituloTic = "Activo/Accesorio";
                valorTic = infoTicket.NombreDetalleActivo;
            }
            body = body.Replace("{nombre}", infoTicket.NombreUsuarioResponsable);
            body = body.Replace("{user}", infoTicket.NombreUsuario);
            body = body.Replace("{tit}", tituloTic);
            body = body.Replace("{act}", valorTic);
            body = body.Replace("{desc}", infoTicket.DescripcionTicket);
            body = body.Replace("{prior}", infoTicket.PrioridadTicket);
            body = body.Replace("{fecha}", infoTicket.FechaAperturaTicket+"");
            body = body.Replace("{year}", DateTime.Now.Year + "");
            return body;
        }
    }
}
