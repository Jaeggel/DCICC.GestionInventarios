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
    public class TipoAccesorioAccDatos
    {
        //Instancia para la utilización de LOGS en la clase TipoAccesorioAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public TipoAccesorioAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con los tipos de accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesTipoAccesorio ObtenerTipoAccesorio(string nombreFuncion)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("TipoAccesorio/ObtenerTipoAccesorio" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los tipos de accesorios: " + e.Message + " - " + msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método para registrar un nuevo tipo de accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio RegistrarTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("TipoAccesorio/RegistrarTipoAccesorio", infoTipoAccesorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un tipo de accesorio: " + e.Message + " - " + msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
        /// <summary>
        /// Método para actualizar un tipo de accesorio en la base de datos.
        /// </summary>
        /// <param name="infoTipoAccesorio"></param>
        /// <returns></returns>
        public MensajesTipoAccesorio ActualizarTipoAccesorio(TipoAccesorio infoTipoAccesorio)
        {
            MensajesTipoAccesorio msjTipoAccesorio = new MensajesTipoAccesorio();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("TipoAccesorio/ActualizarTipoAccesorio", infoTipoAccesorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var TipoAccesorioJson = response.Content.ReadAsStringAsync().Result;
                    msjTipoAccesorio = JsonConvert.DeserializeObject<MensajesTipoAccesorio>(TipoAccesorioJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un tipo de accesorio: " + e.Message + " - " + msjTipoAccesorio.MensajeError);
            }
            return msjTipoAccesorio;
        }
    }
}