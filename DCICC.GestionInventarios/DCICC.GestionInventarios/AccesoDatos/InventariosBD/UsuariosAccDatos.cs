using DCICC.GestionInventarios.Models;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DCICC.GestionInventarios.AccesoDatos.InventariosBD
{
    public class UsuariosAccDatos
    {
        //Instancia para la utilización de LOGS en la clase UsuariosAccDatos
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string token_Autorizacion = string.Empty;
        HttpClient client_Service = new HttpClient();
        ComunicacionServicio obj_ComunicacionServicio = new ComunicacionServicio();
        /// <summary>
        /// Constructor para inicializar el Token de Autorización de Transacciones
        /// </summary>
        /// <param name="NickUsuario_Sesion">Nick del usuario de la sesión actual</param>
        public UsuariosAccDatos(string NickUsuario_Sesion)
        {
            client_Service.DefaultRequestHeaders.Clear();
            client_Service.BaseAddress = new Uri(ComunicacionServicio.base_URL);
            client_Service.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client_Service.DefaultRequestHeaders.Add("Authorization", obj_ComunicacionServicio.ObtenerTokenTransacciones(NickUsuario_Sesion));
        }
        /// <summary>
        /// Constructor para inicializar el Token de Autorización de Inicio de Comunicación con la base de datos.
        /// </summary>
        /// <param name="infoUsuario">Objeto del usuario</param>
        public UsuariosAccDatos(Usuarios infoUsuario)
        {
            token_Autorizacion = obj_ComunicacionServicio.ObtenerTokenInicioBD(infoUsuario);
        }
        /// <summary>
        /// Constructor para acceder a usuarios para recuperar contraseña
        /// </summary>
        public UsuariosAccDatos() { }

        /// <summary>
        /// Método para obtener una lista con los Usuarios Habilitados de la base de datos.
        /// Importante para inicializar los procesos de la base de datos
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuariosHab()
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", token_Autorizacion);
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosHab").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener una lista con todos los usuarios de la base de datos.
        /// Utilizado para la recuperación de contraseña mediante correo electrónico.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuarioCorreo(string infoCorreo)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                ComunicacionServicio objComServ = new ComunicacionServicio();
                HttpClient clientService = new HttpClient();
                clientService.DefaultRequestHeaders.Clear();
                clientService.BaseAddress = new Uri(ComunicacionServicio.base_URL);
                clientService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientService.DefaultRequestHeaders.Add("Authorization", objComServ.ObtenerTokenInicioCorreoBD(infoCorreo));
                HttpResponseMessage response = clientService.GetAsync("Usuarios/ObtenerUsuariosHab").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para obtener una lista todos los Usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesUsuarios ObtenerUsuariosRoles()
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                HttpResponseMessage response = client_Service.GetAsync("Usuarios/ObtenerUsuariosRoles").Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para obtener la lista de todos los usuarios: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para registrar un nuevo Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios RegistrarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Usuarios/RegistrarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para registrar un usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para actualizar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <param name="actPerfil"></param>
        /// <returns></returns>
        public MensajesUsuarios ActualizarUsuario(Usuarios infoUsuario, int opAct)
        {
            string funcionServ = string.Empty;
            if (opAct == 1)
            {
                funcionServ = "Usuarios/ActualizarUsuario";
            }
            else if (opAct == 2)
            {
                funcionServ = "Usuarios/ActualizarEstadoUsuario";
            }
            else if (opAct == 2)
            {
                funcionServ = "Usuarios/ActualizarPerfilUsuario";
            }
            else
            {
                funcionServ = "Usuarios/ActualizarPasswordUsuario";
            }
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync(funcionServ, infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para actualizar un usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
        /// <summary>
        /// Método para eliminar un Usuario en la base de datos.
        /// </summary>
        /// <param name="infoUsuario"></param>
        /// <returns></returns>
        public MensajesUsuarios EliminarUsuario(Usuarios infoUsuario)
        {
            MensajesUsuarios msjUsuarios = new MensajesUsuarios();
            try
            {
                var response = client_Service.PostAsJsonAsync("Usuarios/EliminarUsuario", infoUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = response.Content.ReadAsStringAsync().Result;
                    msjUsuarios = JsonConvert.DeserializeObject<MensajesUsuarios>(usersJson);
                }
            }
            catch (Exception e)
            {
                Logs.Error("Error en la conexión para eliminar un usuario: " + e.Message + " - " + msjUsuarios.MensajeError);
            }
            return msjUsuarios;
        }
    }
}