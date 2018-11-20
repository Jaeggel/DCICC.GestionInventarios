﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesTipoActivo
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesTipoActivo()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo tipo de activo en la base de datos.
        /// </summary>
        /// <param name="infoTipoActivo"></param>
        /// <returns></returns>
        public MensajesTipoActivo RegistroTipoActivo(TipoActivo infoTipoActivo)
        {
            MensajesTipoActivo msjTipoActivo = new MensajesTipoActivo();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO public.dcicc_tipoactivos(id_categoriaact, nombre_tipoact, descripcion_tipoact,vidautil_tipoact, habilitado_tipoact)VALUES(@ic, @nt, @dt, @vua,@ht)", conn_BD))
                {
                    cmd.Parameters.Add("ic", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.IdCategoriaActivo;
                    cmd.Parameters.Add("nt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoTipoActivo.NombreTipoActivo;
                    cmd.Parameters.Add("dt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoTipoActivo.DescripcionTipoActivo) ? (object)infoTipoActivo.DescripcionTipoActivo : DBNull.Value;
                    cmd.Parameters.Add("vua", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoTipoActivo.VidaUtilTipoActivo;
                    cmd.Parameters.Add("ht", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoTipoActivo.HabilitadoTipoActivo;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjTipoActivo.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjTipoActivo.OperacionExitosa = false;
                msjTipoActivo.MensajeError = e.Message;
            }
            return msjTipoActivo;
        }
    }
}