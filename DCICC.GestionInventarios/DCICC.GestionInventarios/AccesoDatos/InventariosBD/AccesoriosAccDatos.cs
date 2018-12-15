using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class AccesoriosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase AccesoriosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public AccesoriosAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con los Accesorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesAccesorios ObtenerAccesorios(string nombreFuncion)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {                
                HttpResponseMessage response = client_Service.GetAsync(string.Format("Accesorios/ObtenerAccesorios{0}",nombreFuncion)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var AccesoriosJson = response.Content.ReadAsStringAsync().Result;
                    msjAccesorios = JsonConvert.DeserializeObject<MensajesAccesorios>(AccesoriosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los accesorios: {0}",e.Message));
            }
            return msjAccesorios;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Accesorio en la base de datos.
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
                Logs.Error(string.Format("Error en la conexión para registrar un accesorio: {0}",e.Message));
            }
            return msjAccesorios;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesAccesorios ActualizarAccesorios(Accesorios infoAccesorios,bool actEstado)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado? "Accesorios/ActualizarEstadoAccesorio":"Accesorios/ActualizarAccesorio", infoAccesorios).Result;
                if (response.IsSuccessStatusCode)
                {
                    var AccesoriosJson = response.Content.ReadAsStringAsync().Result;
                    msjAccesorios = JsonConvert.DeserializeObject<MensajesAccesorios>(AccesoriosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un accesorio: {0}",e.Message));
            }
            return msjAccesorios;
        }
        /// <summary>
        /// Método para actualizar el estado impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="lstAccesorios"></param>
        /// <returns></returns>
        public MensajesCQR ActualizarCQR(List<Accesorios> lstAccesorios)
        {
            MensajesCQR msjActivos = new MensajesCQR();
            try
            {
                var response = client_Service.PostAsJsonAsync("Accesorios/ActualizarCQR", lstAccesorios).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ActivosJson = response.Content.ReadAsStringAsync().Result;
                    msjActivos = JsonConvert.DeserializeObject<MensajesCQR>(ActivosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un CQR: {0}", e.Message));
            }
            return msjActivos;
        }
        #endregion
    }
}