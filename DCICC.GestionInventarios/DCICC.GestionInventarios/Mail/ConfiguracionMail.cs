using DCICC.GestionInventarios.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DCICC.GestionInventarios.Mail
{
    public class ConfiguracionMail
    {
        //Instancia para la utilización de LOGS en la clase Usuarios
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para enviar el correo electrónico para recuperación de contraseña.
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public void SendMail(Correo infoCorreo)
        {
            try
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
                Logs.Info("El correo electrónico ha sido enviado correctamente.");
            }
            catch (Exception e)
            {
                Logs.Error("No se pudo enviar el correo electrónico: "+e.Message);
            }
        }
        /// <summary>
        /// Método para dar formato al Email mediante un archivo HTML (PlantillaEmailPassword.html)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string FormatBodyEmailPassword(Usuarios infoUsuario)
        {
            string path = HttpContext.Current.Server.MapPath("~/Mail/PlantillaEmailPassword.html");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{nombre}", infoUsuario.NombresUsuario);
            body = body.Replace("{user}", infoUsuario.NickUsuario);
            body = body.Replace("{cont}", infoUsuario.PasswordUsuario);
            body = body.Replace("{fecha}", DateTime.Now + "");
            return body;
        }
    }
}