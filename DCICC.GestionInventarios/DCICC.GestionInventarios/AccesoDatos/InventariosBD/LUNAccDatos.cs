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
    public class LUNAccDatos
    {
        //Instancia para la utilización de LOGS en la clase LUNAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public LUNAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con las LUN de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesLUN ObtenerLUN(string nombreFuncion)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("LUN/ObtenerLUN" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var LUNJson = response.Content.ReadAsStringAsync().Result;
                    msjLUN = JsonConvert.DeserializeObject<MensajesLUN>(LUNJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de las LUN: {0}" + e.Message));
            }
            return msjLUN;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar una nueva LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        public MensajesLUN RegistrarLUN(LUN infoLUN)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                var response = client_Service.PostAsJsonAsync("LUN/RegistrarLUN", infoLUN).Result;
                if (response.IsSuccessStatusCode)
                {
                    var LUNJson = response.Content.ReadAsStringAsync().Result;
                    msjLUN = JsonConvert.DeserializeObject<MensajesLUN>(LUNJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar una LUN: {0}", e.Message));
            }
            return msjLUN;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar una LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesLUN ActualizarLUN(LUN infoLUN, bool actEstado)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "LUN/ActualizarEstadoLUN" : "LUN/ActualizarLUN", infoLUN).Result;
                if (response.IsSuccessStatusCode)
                {
                    var LUNJson = response.Content.ReadAsStringAsync().Result;
                    msjLUN = JsonConvert.DeserializeObject<MensajesLUN>(LUNJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar una LUN: {0}", e.Message));
            }
            return msjLUN;
        }
        #endregion
    }
}