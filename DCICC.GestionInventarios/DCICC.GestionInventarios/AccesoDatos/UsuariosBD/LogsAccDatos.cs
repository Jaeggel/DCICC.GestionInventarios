using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos.UsuariosBD
{
    public class LogsAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs4n = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para registrar un nuevo log en la base de datos.
        /// </summary>
        /// <param name="infoLogs"></param>
        /// <returns></returns>
        public MensajesLogs RegistrarLog(Logs infoLogs)
        {
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", ComunicacionServicio.token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Logs/RegistrarLogInicioBD", infoLogs).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjLogs = JsonConvert.DeserializeObject<MensajesLogs>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs4n.Error("Error en la conexión para registrar un log: " + e.Message + " - " + msjLogs.MensajeError);
            }
            return msjLogs;
        }
    }
}