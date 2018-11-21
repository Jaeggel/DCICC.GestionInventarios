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

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class ActivosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase CQRAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public ActivosAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con los activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesActivos ObtenerActivos(string nombreFuncion)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Activos/ObtenerActivos" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los activos: " + e.Message + " - " + msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para registrar un nuevo activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos RegistrarActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Activos/RegistrarActivo", infoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un activo: " + e.Message + " - " + msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para actualizar un activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos ActualizarActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Activos/ActualizarActivo", infoActivo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesActivos>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un activo: " + e.Message + " - " + msjActivos.MensajeError);
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para obtener una lista con los CQR de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesCQR ObtenerCQR()
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("CQR/ObtenerCQR").Result;
                if (response.IsSuccessStatusCode)
                {
                    //var CQRJson = response.Content.ReadAsStringAsync().Result;
                    //msjCQR = JsonConvert.DeserializeObject<MensajesCQR>(CQRJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener el CQR: " + e.Message + " - " + msjCQR.MensajeError);
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para registrar un nuevo CQR en la base de datos.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        public MensajesCQR RegistrarCQR(CQR infoCQR)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("CQR/RegistrarCQR", infoCQR).Result;
                if (response.IsSuccessStatusCode)
                {
                    var CQRJson = response.Content.ReadAsStringAsync().Result;
                    msjCQR = JsonConvert.DeserializeObject<MensajesCQR>(CQRJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un CQR: " + e.Message + " - " + msjCQR.MensajeError);
            }
            return msjCQR;
        }
    }
}