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
    public class EncriptacionAccDatos
    {
        //Instancia para la utilización de LOGS en la clase EncriptacionAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public EncriptacionAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        #endregion
        #region Encriptación/Desencriptación
        /// <summary>
        /// Método para desencriptar una cadena en particular.
        /// </summary>
        /// <param name="valorEncriptado"></param>
        /// <returns></returns>
        public string Desencriptar(string valorEncriptado)
        {
            string valorSinEncriptar=string.Empty;
            try
            {
                var response = client_Service.PostAsJsonAsync("Encriptacion/Desencriptar", valorEncriptado).Result;
                if (response.IsSuccessStatusCode)
                {
                    var valorJson = response.Content.ReadAsStringAsync().Result;
                    valorSinEncriptar = JsonConvert.DeserializeObject<string>(valorJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para desencriptar un valor: {0}", e.Message));
            }
            return valorSinEncriptar;
        }
        /// <summary>
        /// Método para encriptar una cadena en particular.
        /// </summary>
        /// <param name="valorSinEncriptar"></param>
        /// <returns></returns>
        public string Encriptar(string valorSinEncriptar)
        {
            string valorEncriptado = string.Empty;
            try
            {
                var response = client_Service.PostAsJsonAsync("Encriptacion/Encriptar", valorSinEncriptar).Result;
                if (response.IsSuccessStatusCode)
                {
                    var valorJson = response.Content.ReadAsStringAsync().Result;
                    valorEncriptado = JsonConvert.DeserializeObject<string>(valorJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para encriptar un valor: {0}", e.Message));
            }
            return valorEncriptado;
        }
        #endregion
    }
}