using DCICC.GestionInventarios.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos
{
    public class ComunicacionServicio
    {
        //Instancia para la utilización de LOGS en la clase ComunicacionServicio
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string base_URL;
        /// <summary>
        /// Constructor para obtener el URL de la ubicación del Web Service (DCICC.WebServiceInventarios).
        /// </summary>
        public ComunicacionServicio()
        {
            base_URL = ConfigurationManager.AppSettings["URLWebServiceInventarios"];
        }
        /// <summary>
        /// Método para obtener el Token de autenticación para poder realizar las operaciones en el Servicio REST.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public string ObtenerTokenInicioBD(Usuarios infoUsuario)
        {
            string tokenResult = string.Empty;
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = clientService.PostAsJsonAsync("Token/ObtenerTokenInicioBD", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var tokenJson = response.Content.ReadAsStringAsync().Result;
                    tokenResult = JsonConvert.DeserializeObject<string>(tokenJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener el token de autorización: " + e.Message);
                tokenResult= null;
            }
            return tokenResult;
        }
        /// <summary>
        /// Método para obtener el Token de autenticación para poder realizar las operaciones en el Servicio REST.
        /// </summary>
        /// <param name="infoCorreo"></param>
        /// <returns></returns>
        public string ObtenerTokenInicioCorreoBD(string infoCorreo)
        {
            string tokenResult = string.Empty;
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = clientService.PostAsJsonAsync("Token/ObtenerTokenInicioCorreoBD", infoCorreo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var tokenJson = response.Content.ReadAsStringAsync().Result;
                    tokenResult = JsonConvert.DeserializeObject<string>(tokenJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener el token de autorización: " + e.Message);
                tokenResult = null;
            }
            return tokenResult;
        }
        /// <summary>
        /// Método para obtener el Token de autenticación para poder realizar las operaciones en el Servicio REST.
        /// </summary>
        /// <returns></returns>
        public string ObtenerTokenTransacciones(string NickUsuarioSesion)
        {
            string tokenResult = string.Empty;
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = clientService.PostAsJsonAsync("Token/ObtenerTokenTransacciones", NickUsuarioSesion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var tokenJson = response.Content.ReadAsStringAsync().Result;
                    tokenResult = JsonConvert.DeserializeObject<string>(tokenJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener el token de autorización: " + e.Message);
                tokenResult=null;
            }
            return tokenResult;
        }
    }
}