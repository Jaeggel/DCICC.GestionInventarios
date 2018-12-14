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
    public class StorageAccDatos
    {
        //Instancia para la utilización de LOGS en la clase StorageAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public StorageAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con los Storage de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab(Los registros habilitados)</ param >
        /// <returns></returns>
        public MensajesStorage ObtenerStorage(string nombreFuncion)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync(string.Format("Storage/ObtenerStorage{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjStorage = JsonConvert.DeserializeObject<MensajesStorage>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los Storage: {0}", e.Message));
            }
            return msjStorage;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <returns></returns>
        public MensajesStorage RegistrarStorage(Storage infoStorage)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                var response = client_Service.PostAsJsonAsync("Storage/RegistrarStorage", infoStorage).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjStorage = JsonConvert.DeserializeObject<MensajesStorage>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un Storage: {0}", e.Message));
            }
            return msjStorage;
        }
        #endregion
        #region Actualiaciones
        /// <summary>
        /// Método para actualizar un Storage en la base de datos.
        /// </summary>
        /// <param name="infoStorage"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesStorage ActualizarStorage(Storage infoStorage, bool actEstado)
        {
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "Storage/ActualizarEstadoStorage" : "Storage/ActualizarStorage", infoStorage).Result;
                if (response.IsSuccessStatusCode)
                {
                    var catJson = response.Content.ReadAsStringAsync().Result;
                    msjStorage = JsonConvert.DeserializeObject<MensajesStorage>(catJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un Storage: {0}" + e.Message));
            }
            return msjStorage;
        }
        #endregion
    }
}