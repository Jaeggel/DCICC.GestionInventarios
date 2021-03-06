﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesLaboratorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesLaboratorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        public MensajesLaboratorios ActualizacionLaboratorio(Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_laboratorios set nombre_laboratorio = @nl,descripcion_laboratorio=@dl,ubicacion_laboratorio=@ul,habilitado_laboratorio = @hl where id_laboratorio = @il", conn_BD))
                {
                    cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLaboratorio.NombreLaboratorio.Trim();
                    cmd.Parameters.Add("ul", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLaboratorio.UbicacionLaboratorio.Trim();
                    cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoLaboratorio.DescripcionLaboratorio) ? (object)infoLaboratorio.DescripcionLaboratorio.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLaboratorio.HabilitadoLaboratorio;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLaboratorio.IdLaboratorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = false;
                msjLaboratorios.MensajeError = e.Message;
            }
            return msjLaboratorios;
        }
        /// <summary>
        /// Método para actualizar el estado de un Laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        public MensajesLaboratorios ActualizacionEstadoLaboratorio(Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE dcicc_laboratorios set habilitado_laboratorio = @hl where id_laboratorio = @il", conn_BD))
                {
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLaboratorio.HabilitadoLaboratorio;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoLaboratorio.IdLaboratorio;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = false;
                msjLaboratorios.MensajeError = e.Message;
            }
            return msjLaboratorios;
        }
    }
}
