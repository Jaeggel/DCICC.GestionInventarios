using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesAccesorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesAccesorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios ActualizacionAccesorio(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_accesorio SET id_tipoaccesorio=@ita, nombre_accesorio=@na, serial_accesorio=@sa, modelo_accesorio=@ma, descripcion_accesorio=@da, estado_accesorio=@ea WHERE id_accesorio=@ia;", conn_BD))
                {
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdTipoAccesorio;
                    cmd.Parameters.Add("na", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.NombreAccesorio;
                    cmd.Parameters.Add("sa", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.SerialAccesorio) ? (object)infoAccesorios.SerialAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ma", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.ModeloAccesorio) ? (object)infoAccesorios.ModeloAccesorio : DBNull.Value;
                    cmd.Parameters.Add("da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.DescripcionAccesorio) ? (object)infoAccesorios.DescripcionAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ea", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.EstadoAccesorio;
                    cmd.Parameters.Add("ia", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdAccesorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
        /// <summary>
        /// Método para actualizar el estado de un Accesorio en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios ActualizacionEstadoAccesorio(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_accesorio SET estado_accesorio=@ea WHERE id_accesorio=@ia;", conn_BD))
                {
                    cmd.Parameters.Add("ea", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.EstadoAccesorio;
                    cmd.Parameters.Add("ia", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdAccesorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
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
        /// <summary>
        /// Método para actualizar el estado de impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesCQR ActualizacionQR(List<Accesorios> lstAccesorios)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                foreach (var item in lstAccesorios)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_cqr SET impreso_cqr=@imcq WHERE id_cqr=@icq;", conn_BD))
                    {
                        cmd.Parameters.Add("imcq", NpgsqlTypes.NpgsqlDbType.Boolean).Value = true;
                        cmd.Parameters.Add("icq", NpgsqlTypes.NpgsqlDbType.Varchar).Value = item.IdCQR;
                        cmd.ExecuteNonQuery();
                    }
                }
                tran.Commit();
                conn_BD.Close();
                msjCQR.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjCQR.OperacionExitosa = false;
                msjCQR.MensajeError = e.Message;
            }
            return msjCQR;
        }
    }
}
