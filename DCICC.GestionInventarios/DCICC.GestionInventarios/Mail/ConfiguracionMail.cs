using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.IO;
using System.Net.Mail;
using System.Web;

namespace DCICC.GestionInventarios.Mail
{
    public class ConfiguracionMail
    {
        /// <summary>
        /// Método para enviar el Correo Electrónico para recuperación de Contraseña.
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
        /// Método para dar formato al Email mediante un archivo HTML (PlantillaEmailPassword.html)
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public string FormatBodyEmailPassword(Usuarios infoUsuario)
        {
            string path = HttpContext.Current.Server.MapPath("~/Mail/PlantillaEmailPassword.html");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            EncriptacionAccDatos objEncriptacionBD = new EncriptacionAccDatos(infoUsuario.NickUsuario);
            body = body.Replace("{nombre}", infoUsuario.NombresUsuario);
            body = body.Replace("{user}", infoUsuario.NickUsuario);
            body = body.Replace("{cont}", objEncriptacionBD.Desencriptar(infoUsuario.PasswordUsuario));
            body = body.Replace("{fecha}", DateTime.Now + "");
            body = body.Replace("{year}", DateTime.Now.Year + "");
            return body;
        }
        /// <summary>
        /// Método para dar formato al Email mediante un archivo HTML (InfoTicket.html)
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public string FormatBodyTicket(Tickets infoTicket)
        {
            string body = string.Empty;
            string path = HttpContext.Current.Server.MapPath("~/Mail/InfoTicket.html");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{nombre}", infoTicket.NombreUsuarioResponsable);
            body = body.Replace("{act}", infoTicket.NombreDetalleActivo);
            body = body.Replace("{user}", infoTicket.NombreUsuario);
            body = body.Replace("{desc}", infoTicket.ComentarioTicket);
            body = body.Replace("{prior}", infoTicket.PrioridadTicket);
            body = body.Replace("{fecha}", infoTicket.FechaAperturaTicket + "");
            body = body.Replace("{year}", DateTime.Now.Year + "");
            return body;
        }
    }
}