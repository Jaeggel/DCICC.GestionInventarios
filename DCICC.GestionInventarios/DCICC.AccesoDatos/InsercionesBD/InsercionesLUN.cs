using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesLUN
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesLUN()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar una nueva LUN en la base de datos.
        /// </summary>
        /// <param name="infoLUN"></param>
        /// <returns></returns>
        public MensajesLUN RegistroLUN(LUN infoLUN)
        {
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into dcicc_LUN (nombre_LUN,descripcion_LUN,habilitado_LUN) VALUES (@nm,@dm,@hm)", conn_BD))
                {
                    //cmd.Parameters.Add("nm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLUN.NombreLUN;
                    //cmd.Parameters.Add("dm", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoLUN.DescripcionLUN) ? (object)infoLUN.DescripcionLUN : DBNull.Value;
                    //cmd.Parameters.Add("hm", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLUN.HabilitadoLUN;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjLUN.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLUN.OperacionExitosa = false;
                msjLUN.MensajeError = e.Message;
            }
            return msjLUN;
        }
    }
}
