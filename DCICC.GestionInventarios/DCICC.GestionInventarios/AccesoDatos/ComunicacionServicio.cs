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
using System.Web;

namespace DCICC.GestionInventarios.AccesoDatos
{
    public class ComunicacionServicio
    {
        //Instancia para la utilización de LOGS en la clase ComunicacionServicio
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string base_URL;
        public static string token_Autorizacion = string.Empty;
        /// <summary>
        /// Constructor para obtener el URL de la ubicación del Web Service (DCICC.WebServiceInventarios).
        /// Además, de la inicialización del Token de autorización.
        /// </summary>
        public ComunicacionServicio(Login infoLogin)
        {
            base_URL = ConfigurationManager.AppSettings["URLWebServiceInventarios"];
            token_Autorizacion = "Bearer " + ObtenerTokenAutorizacion(infoLogin);
        }
        /// <summary>
        /// Método para obtener el Token de autenticación para poder realizar las operaciones en el Servicio REST.
        /// </summary>
        /// <returns></returns>
        public string ObtenerTokenAutorizacion(Login infoLogin)
        {
            string tokenResult = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(base_URL + "Token/ObtenerToken");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonInfoLogin = JsonConvert.SerializeObject(infoLogin);
                    streamWriter.Write(jsonInfoLogin);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    tokenResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener el token de autorización: " + e.Message);
                return null;
            }
            return tokenResult;
        }
    }
}