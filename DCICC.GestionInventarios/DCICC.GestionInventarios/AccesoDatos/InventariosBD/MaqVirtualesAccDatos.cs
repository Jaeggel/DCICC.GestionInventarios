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
    public class MaqVirtualesAccDatos
    {
        //Instancia para la utilización de LOGS en la clase MaqVirtualesAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        public MaqVirtualesAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            token_Autorizacion = "Bearer " + objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion);
        }
        /// <summary>
        /// Método para obtener una lista con las máquinas virtuales de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesMaqVirtuales ObtenerMaqVirtuales(string nombreFuncion)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("MaqVirtuales/ObtenerMaqVirtuales" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MaqVirtualesJson = response.Content.ReadAsStringAsync().Result;
                    msjMaqVirtuales = JsonConvert.DeserializeObject<MensajesMaqVirtuales>(MaqVirtualesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de las máquinas virtuales: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método para registrar una nueva máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales RegistrarMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("MaqVirtuales/RegistrarMaqVirtual", infoMaqVirtual).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MaqVirtualesJson = response.Content.ReadAsStringAsync().Result;
                    msjMaqVirtuales = JsonConvert.DeserializeObject<MensajesMaqVirtuales>(MaqVirtualesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar una máquina virtual: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
        /// <summary>
        /// Método para actualizar una máquina virtual en la base de datos.
        /// </summary>
        /// <param name="infoMaqVirtual"></param>
        /// <returns></returns>
        public MensajesMaqVirtuales ActualizarMaqVirtual(MaqVirtuales infoMaqVirtual)
        {
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                var response = clientService.PostAsJsonAsync("MaqVirtuales/ActualizarMaqVirtual", infoMaqVirtual).Result;
                if (response.IsSuccessStatusCode)
                {
                    var MaqVirtualesJson = response.Content.ReadAsStringAsync().Result;
                    msjMaqVirtuales = JsonConvert.DeserializeObject<MensajesMaqVirtuales>(MaqVirtualesJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar una máquina virtual: " + e.Message + " - " + msjMaqVirtuales.MensajeError);
            }
            return msjMaqVirtuales;
        }
    }
}