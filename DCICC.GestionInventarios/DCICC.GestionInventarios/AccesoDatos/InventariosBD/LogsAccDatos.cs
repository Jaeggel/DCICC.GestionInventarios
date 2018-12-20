using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class LogsAccDatos
    {
        //Instancia para la utilización de LOGS en la clase LogsAccDatos
        private static readonly ILog Logs4n = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public LogsAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        #endregion
        #region Consultas
        /// <summary>
        /// Método para obtener una lista completa de los Logs de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesLogs ObtenerLogs(string nombreFuncion)
        {
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(!string.IsNullOrEmpty(nombreFuncion) ? string.Format("Logs/ObtenerLogs{0}",nombreFuncion):"Logs/ObtenerLogs").Result;
                if (response.IsSuccessStatusCode)
                {
                    var logsJson = response.Content.ReadAsStringAsync().Result;
                    msjLogs = JsonConvert.DeserializeObject<MensajesLogs>(logsJson);
                }
            }
            catch (Exception e)
            {
                Logs4n.Error(string.Format("Error en la conexión para obtener la lista de todos los logs: {0}",e.Message));
            }
            return msjLogs;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Log en la base de datos.
        /// </summary>
        /// <param name="infoLogs"></param>
        /// <returns></returns>
        public MensajesLogs RegistrarLog(Logs infoLogs)
        {
            MensajesLogs msjLogs = new MensajesLogs();
            try
            {
                var response = client_Service.PostAsJsonAsync("Logs/RegistrarLog", infoLogs).Result;
                if (response.IsSuccessStatusCode)
                {
                    var logsJson = response.Content.ReadAsStringAsync().Result;
                    msjLogs = JsonConvert.DeserializeObject<MensajesLogs>(logsJson);
                }
            }
            catch (Exception e)
            {
                Logs4n.Error(string.Format("Error en la conexión para registrar un log: {0}",e.Message));
            }
            return msjLogs;
        }
        #endregion
    }
}