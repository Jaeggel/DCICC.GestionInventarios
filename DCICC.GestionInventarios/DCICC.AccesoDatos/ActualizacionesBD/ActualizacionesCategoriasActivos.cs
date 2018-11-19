﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesCategoriasActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesCategoriasActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar una categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoriaActivo"></param>
        /// <returns></returns>
        public MensajesCategoriasActivos ActualizacionCategoria(CategoriaActivo infoCategoriaActivo)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (var cmd = new NpgsqlCommand("UPDATE dcicc_categoriaactivos set nombre_categoriaact = @nc,descripcion_categoriaact=@dc,habilitado_categoriaact = @hc where id_categoriaact = @ic", conn_BD))
                {
                    cmd.Parameters.Add("nc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoCategoriaActivo.NombreCategoriaActivo;
                    cmd.Parameters.Add("dc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoCategoriaActivo.DescripcionCategoriaActivo;
                    cmd.Parameters.Add("hc", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoCategoriaActivo.HabilitadoCategoriaActivo;
                    cmd.Parameters.Add("ic", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoCategoriaActivo.IdCategoriaActivo;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjCategorias.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjCategorias.OperacionExitosa = false;
                msjCategorias.MensajeError = e.Message;
            }
            return msjCategorias;
        }
    }
}