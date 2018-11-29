using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesAccesorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesAccesorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios RegistroAccesorio(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO public.dcicc_accesorio(id_tipoaccesorio, id_detalleact, nombre_accesorio, serial_accesorio, modelo_accesorio, descripcion_accesorio, estado_accesorio)VALUES (@ita, @ida,@na, @sa, @ma, @da, @ea);", conn_BD))
                {
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdTipoAccesorio;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdDetalleActivo;
                    cmd.Parameters.Add("na", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.NombreAccesorio;
                    cmd.Parameters.Add("sa", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.SerialAccesorio) ? (object)infoAccesorios.SerialAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ma", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.ModeloAccesorio) ? (object)infoAccesorios.ModeloAccesorio: DBNull.Value;
                    cmd.Parameters.Add("da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.DescripcionAccesorio) ? (object)infoAccesorios.DescripcionAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ea", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.EstadoAccesorio;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjAccesorios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjAccesorios.OperacionExitosa = false;
                msjAccesorios.MensajeError = e.Message;
            }
            return msjAccesorios;
        }
    }
}
