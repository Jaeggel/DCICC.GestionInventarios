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
    public class AccesoriosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase AccesoriosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        public AccesoriosAccDatos(string NickUsuario_Sesion)
        {
            ComunicacionServicio objComunicacionServicio = new ComunicacionServicio();
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", objComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        /// <summary>
        /// Método para obtener una lista con los accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesAccesorios ObtenerAccesorios(string nombreFuncion)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {                
                HttpResponseMessage response = client_Service.GetAsync("Accesorios/ObtenerAccesorios" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var AccesoriosJson = response.Content.ReadAsStringAsync().Result;
                    msjAccesorios = JsonConvert.DeserializeObject<MensajesAccesorios>(AccesoriosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de los accesorios: " + e.Message + " - " + msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método para registrar una nuevo accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios RegistrarAccesorios(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Accesorios/RegistrarAccesorio", infoAccesorios).Result;
                if (response.IsSuccessStatusCode)
                {
                    var AccesoriosJson = response.Content.ReadAsStringAsync().Result;
                    msjAccesorios = JsonConvert.DeserializeObject<MensajesAccesorios>(AccesoriosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un accesorio: " + e.Message + " - " + msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método para actualizar un accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios ActualizarAccesorios(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Accesorios/ActualizarAccesorio", infoAccesorios).Result;
                if (response.IsSuccessStatusCode)
                {
                    var AccesoriosJson = response.Content.ReadAsStringAsync().Result;
                    msjAccesorios = JsonConvert.DeserializeObject<MensajesAccesorios>(AccesoriosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un accesorio: " + e.Message + " - " + msjAccesorios.MensajeError);
            }
            return msjAccesorios;
        }
    }
}