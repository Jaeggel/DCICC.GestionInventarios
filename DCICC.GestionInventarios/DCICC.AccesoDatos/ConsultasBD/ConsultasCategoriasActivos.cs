using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasCategoriasActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasCategoriasActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener las categorias de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultacategoriaactivos o categoriashabilitadas</param></param>
        /// <returns></returns>
        public MensajesCategoriasActivos ObtenerCategoriasActivos(string nombreFuncion)
        {
            List<CategoriaActivo> lstCategorias = new List<CategoriaActivo>();
            MensajesCategoriasActivos msjCategorias = new MensajesCategoriasActivos();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CategoriaActivo objCategorias = new CategoriaActivo
                            {
                                IdCategoriaActivo = Int32.Parse(dr[0].ToString().Trim()),
                                NombreCategoriaActivo = dr[1].ToString().Trim(),
                                DescripcionCategoriaActivo = dr[2].ToString().Trim(),
                                HabilitadoCategoriaActivo = Boolean.Parse(dr[3].ToString().Trim())
                            };
                            lstCategorias.Add(objCategorias);
                        }
                        conn_BD.Close();
                        msjCategorias.ListaObjetoInventarios = lstCategorias;
                        msjCategorias.OperacionExitosa = true;
                    }
                }
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
