using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesMarcas
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesMarcas()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar una Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas ActualizacionMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_marca set nombre_marca = @nm,descripcion_marca=@dm,habilitado_marca = @hm where id_marca = @im", conn_BD))
                {
                    cmd.Parameters.Add("nm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMarca.NombreMarca;
                    cmd.Parameters.Add("dm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoMarca.DescripcionMarca) ? (object)infoMarca.DescripcionMarca : DBNull.Value;
                    cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMarca.HabilitadoMarca;
                    cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMarca.IdMarca;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjMarcas.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjMarcas.OperacionExitosa = false;
                msjMarcas.MensajeError = e.Message;
            }
            return msjMarcas;
        }
        /// <summary>
        /// Método para actualizar el estado de una Marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas ActualizacionEstadoMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_marca set habilitado_marca = @hm where id_marca = @im", conn_BD))
                {
                    cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMarca.HabilitadoMarca;
                    cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoMarca.IdMarca;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjMarcas.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjMarcas.OperacionExitosa = false;
                msjMarcas.MensajeError = e.Message;
            }
            return msjMarcas;
        }
    }
}
