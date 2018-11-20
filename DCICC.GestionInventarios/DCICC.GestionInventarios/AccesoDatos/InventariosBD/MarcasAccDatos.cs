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
    public class MarcasAccDatos
    {
        //Instancia para la utilización de LOGS en la clase MarcasAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public MarcasAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con las marcas de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesMarcas ObtenerMarcas(string nombreFuncion)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Marcas/ObtenerMarcas" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de las marcas: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método para registrar una nueva marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas RegistrarMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Marcas/RegistrarMarca", infoMarca).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar una marca: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método para actualizar una marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas ActualizarMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("Marcas/ActualizarMarca", infoMarca).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MarcasJson = response.Content.ReadAsStringAsync().Result;
                    msjMarcas = JsonConvert.DeserializeObject<MensajesMarcas>(MarcasJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar una marca: " + e.Message + " - " + msjMarcas.MensajeError);
            }
            return msjMarcas;
        }
    }
}