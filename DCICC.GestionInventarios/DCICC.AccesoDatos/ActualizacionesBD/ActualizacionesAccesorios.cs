﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Método para actualizar una Accesorios en la base de datos.
        /// </summary>
        /// <param name="infoAccesorios"></param>
        /// <returns></returns>
        public MensajesAccesorios ActualizacionAccesorios(Accesorios infoAccesorios)
        {
            MensajesAccesorios msjAccesorios = new MensajesAccesorios();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE public.dcicc_accesorio SET id_tipoaccesorio=@ita, id_detalleact=@ida, nombre_accesorio=@na, serial_accesorio=@sa, modelo_accesorio=@ma, descripcion_accesorio=@da, habilitado_accesorio=@ha WHERE id_accesorio=@ia;", conn_BD))
                {
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdTipoAccesorio;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoAccesorios.IdDetalleActivo;
                    cmd.Parameters.Add("na", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoAccesorios.NombreAccesorio;
                    cmd.Parameters.Add("sa", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.SerialAccesorio) ? (object)infoAccesorios.SerialAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ma", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.ModeloAccesorio) ? (object)infoAccesorios.ModeloAccesorio : DBNull.Value;
                    cmd.Parameters.Add("da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoAccesorios.DescripcionAccesorio) ? (object)infoAccesorios.DescripcionAccesorio : DBNull.Value;
                    cmd.Parameters.Add("ha", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoAccesorios.HabilitadoAccesorio;
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
    }
}