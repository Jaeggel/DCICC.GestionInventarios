using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class SistOperativosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase SistOperativosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient client_Service = new HttpClient();
        #region Constructor Comunicación Servicio
        /// <summary>
        /// Constructor para configurar la comunicación con el Web Service
        /// </summary>
        /// <param name="NickUsuario_Sesion"></param>
        public SistOperativosAccDatos(string NickUsuario_Sesion)
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
        /// Método para obtener una lista con los Sistemas Operativos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función: Comp(Todos los registros) o Hab (Los registros habilitados)</param>
        /// <returns></returns>
        public MensajesSistOperativos ObtenerSistOperativos(string nombreFuncion)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("SistOperativos/ObtenerSistOperativos" + nombreFuncion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var SistOperativosJson = response.Content.ReadAsStringAsync().Result;
                    msjSistOperativos = JsonConvert.DeserializeObject<MensajesSistOperativos>(SistOperativosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para obtener la lista de los sistemas operativos: {0}.",e.Message));
            }
            return msjSistOperativos;
        }
        #endregion
        #region Registros
        /// <summary>
        /// Método para registrar un nuevo Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <returns></returns>
        public MensajesSistOperativos RegistrarSistOperativo(SistOperativos infoSistOperativo)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                var response = client_Service.PostAsJsonAsync("SistOperativos/RegistrarSistOperativo", infoSistOperativo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var SistOperativosJson = response.Content.ReadAsStringAsync().Result;
                    msjSistOperativos = JsonConvert.DeserializeObject<MensajesSistOperativos>(SistOperativosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para registrar un sistema operativo: {0}.",e.Message));
            }
            return msjSistOperativos;
        }
        #endregion
        #region Actualizaciones
        /// <summary>
        /// Método para actualizar un Sistema Operativo en la base de datos.
        /// </summary>
        /// <param name="infoSistOperativo"></param>
        /// <param name="actEstado">Boolean para definir si se actualizará solo el estado o todo el registro</param>
        /// <returns></returns>
        public MensajesSistOperativos ActualizarSistOperativo(SistOperativos infoSistOperativo,bool actEstado)
        {
            MensajesSistOperativos msjSistOperativos = new MensajesSistOperativos();
            try
            {
                var response = client_Service.PostAsJsonAsync(actEstado ? "SistOperativos/ActualizarEstadoSistOperativo":"SistOperativos/ActualizarSistOperativo", infoSistOperativo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var SistOperativosJson = response.Content.ReadAsStringAsync().Result;
                    msjSistOperativos = JsonConvert.DeserializeObject<MensajesSistOperativos>(SistOperativosJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("Error en la conexión para actualizar un sistema operativo: {0}.",e.Message));
            }
            return msjSistOperativos;
        }
        #endregion
    }
}