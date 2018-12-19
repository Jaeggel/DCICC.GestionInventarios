using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class DashboardAccDatos
    {
        //Instancia para la utilización de Dashboard en la clase DashboardAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public DashboardAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener información sobre el dashboard ubicado en Top.
        /// </summary>
        /// <param name="nombreRol"></param>
        /// <returns></returns>
        public MensajesDashboard ObtenerDashboardTop(string nickUsuario)
        {
            MensajesDashboard msjDashboard = new MensajesDashboard();
            try
            {
                var response = client_Service.PostAsJsonAsync("Dashboard/ObtenerDashboardTop", nickUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dsbdJson = response.Content.ReadAsStringAsync().Result;
                    msjDashboard = JsonConvert.DeserializeObject<MensajesDashboard>(dsbdJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener los permisos de un Rol: {0}", e.Message));
            }
            return msjDashboard;
        }
        #endregion
    }
}