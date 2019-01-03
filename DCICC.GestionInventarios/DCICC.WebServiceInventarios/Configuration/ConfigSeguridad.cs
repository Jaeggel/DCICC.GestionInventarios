using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DCICC.WebServiceInventarios.Configuration
{
    public class ConfigSeguridad
    {
        //Instancia para la utilización de LOGS en la clase ConfigSeguridad
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ConfigurationBuilder configuration_Builder = new ConfigurationBuilder();
        string path_ArchivoConfig = string.Empty;
        IConfigurationRoot root_Config;
        /// <summary>
        /// Constructor para llamar a los métodos que se encargan de obtener 
        /// </summary>
        public ConfigSeguridad()
        {
            configuration_Builder = new ConfigurationBuilder();
            path_ArchivoConfig = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configuration_Builder.AddJsonFile(path_ArchivoConfig, false);
            root_Config = configuration_Builder.Build();
        }
        /// <summary>
        /// Método para obtener la llave de encriptación del algoritmo a usar (AES) desde el archivo de configuración appsettings.json.
        /// </summary>
        /// <returns></returns>
        public string ObtenerEncryptionKey()
        {
            string llaveEncriptacion = string.Empty;
            try
            {
                llaveEncriptacion = root_Config.GetConnectionString("EncryptionKey");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo obtener el EncriptionKey del archivo de configuración: {0}.",e.Message));
                llaveEncriptacion = null;
            }
            return llaveEncriptacion;
        }
        /// <summary>
        /// Método para obtener la cadena de conexión desde el archivo de configuración appsettings.json
        /// </summary>
        /// <returns></returns>
        public string ObtenerCadenaConexion()
        {
            string cadenaConexion = string.Empty;
            try
            {
                cadenaConexion=root_Config.GetConnectionString("BDStrConnection");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo obtener la cadena de conexión del archivo de configuración: {0}.", e.Message));
                cadenaConexion = null;
            }
            return cadenaConexion;
        }
        /// <summary>
        /// Método para obtener el email emisor desde el archivo de configuración appsettings.json
        /// </summary>
        /// <returns></returns>
        public string ObtenerEmailEmisor()
        {
            string emailEmisor = string.Empty;
            try
            {
                emailEmisor = root_Config.GetConnectionString("EmailEmisor");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo obtener el email emisor del archivo de configuración: {0}.", e.Message));
                emailEmisor = null;
            }
            return emailEmisor;
        }
        /// <summary>
        /// Método para obtener el email emisor desde el archivo de configuración appsettings.json
        /// </summary>
        /// <returns></returns>
        public string ObtenerClaveEmailEmisor()
        {
            string claveEmailEmisor = string.Empty;
            try
            {
                claveEmailEmisor = root_Config.GetConnectionString("ClaveEmailEmisor");
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo obtener el email emisor del archivo de configuración: {0}.", e.Message));
                claveEmailEmisor = null;
            }
            return claveEmailEmisor;
        }
        /// <summary>
        /// Método para obtener el tiempo en minutos en el cual expirará el Token de petición desde el archivo de configuración appsettings.json.
        /// </summary>
        /// <returns></returns>
        public int ObtenerTimeExpToken()
        {
            int time = 0;
            try
            {
                time = Convert.ToInt32(root_Config.GetConnectionString("ExpirationTimeToken"));
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se pudo obtener el ExpirationTimeToken del archivo de configuración: {0}.",e.Message));
                time = 0;
            }
            return time;
        }
    }
}
