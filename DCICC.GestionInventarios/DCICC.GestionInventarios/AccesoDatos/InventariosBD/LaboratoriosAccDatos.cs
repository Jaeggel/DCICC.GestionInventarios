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
    public class LaboratoriosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase LaboratoriosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        public LaboratoriosAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        /// <summary>
        /// Método para obtener una lista con los laboratorios habilitados de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab(Los registros habilitados)</ param >
        /// <returns></returns>
        public MensajesLaboratorios ObtenerLaboratorios(string nombreFuncion)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Laboratorios/ObtenerLaboratorios" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjLaboratorios = JsonConvert.DeserializeObject<MensajesLaboratorios>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los laboratorios: " + e.Message + " - " + msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método para registrar un nuevo laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        public MensajesLaboratorios RegistrarLaboratorio(Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Laboratorios/RegistrarLaboratorio", infoLaboratorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjLaboratorios = JsonConvert.DeserializeObject<MensajesLaboratorios>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un laboratorio: " + e.Message + " - " + msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método para actualizar un laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        public MensajesLaboratorios ActualizarLaboratorio(Laboratorios infoLaboratorio, bool actEstado)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "Laboratorios/ActualizarEstadoLaboratorio" : "Laboratorios/ActualizarLaboratorio", infoLaboratorio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjLaboratorios = JsonConvert.DeserializeObject<MensajesLaboratorios>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un laboratorio: " + e.Message + " - " + msjLaboratorios.MensajeError);
            }
            return msjLaboratorios;
        }
    }
}