﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesCategoriasActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesCategoriasActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nueva Categoría en la base de datos.
        /// </summary>
        /// <param name="infoCategoria"></param>
        /// <returns></returns>
        public MensajesCategoriasActivos RegistroCategoria(CategoriaActivo infoCategoria)
        {
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_categoriaactivos (nombre_categoriaact,descripcion_categoriaact,habilitado_categoriaact) VALUES (@nc,@dc,@hc)", conn_BD))
                {
                    cmd.Parameters.Add("nc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoCategoria.NombreCategoriaActivo.Trim();
                    cmd.Parameters.Add("dc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoCategoria.DescripcionCategoriaActivo) ? (object)infoCategoria.DescripcionCategoriaActivo.Trim() : DBNull.Value;
                    cmd.Parameters.AddWithValue("hc", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoCategoria.HabilitadoCategoriaActivo;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjCategorias.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjCategorias.OperacionExitosa = false;
                msjCategorias.MensajeError = e.Message;
            }
            return msjCategorias;
        }
    }
}
