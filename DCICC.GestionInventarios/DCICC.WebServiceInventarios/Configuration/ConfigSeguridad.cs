using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DCICC.WebServiceInventarios.Configuration
{
    public class ConfigSeguridad
    {
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
            catch (Exception)
            {
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
            return root_Config.GetConnectionString("BDStrConnection");
        }
        /// <summary>
        /// Método para obtener el tiempo en minutos en el cual expirará el Token de petición desde el archivo de configuración appsettings.json.
        /// </summary>
        /// <returns></returns>
        public int getTimeExpToken()
        {
            int time = 0;
            try
            {
                time = Convert.ToInt32(root_Config.GetConnectionString("ExpirationTimeToken"));
            }
            catch (Exception)
            {
                time = 0;
            }
            return time;
        }
    }
}
