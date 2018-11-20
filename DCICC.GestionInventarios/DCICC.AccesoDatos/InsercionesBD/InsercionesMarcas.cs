using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesMarcas
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesMarcas()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nueva marca en la base de datos.
        /// </summary>
        /// <param name="infoMarca"></param>
        /// <returns></returns>
        public MensajesMarcas RegistroMarca(Marcas infoMarca)
        {
            MensajesMarcas msjMarcas = new MensajesMarcas();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_marca (nombre_marca,descripcion_marca,habilitado_marca) VALUES (@nm,@dm,@hm)", conn_BD))
                {
                    cmd.Parameters.Add("nm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoMarca.NombreMarca;
                    cmd.Parameters.Add("dm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoMarca.DescripcionMarca) ? (object)infoMarca.DescripcionMarca : DBNull.Value;
                    cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoMarca.HabilitadoMarca;
                    cmd.ExecuteNonQuery();
                }
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
