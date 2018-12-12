using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasLUN
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasLUN()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener las LUN de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaLUN o LUNhabilitados</param>
        /// <returns></returns>
        public MensajesLUN ObtenerLUN(string nombreFuncion)
        {
            List<LUN> lstLUN = new List<LUN>();
            MensajesLUN msjLUN = new MensajesLUN();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //LUN objLUN = new LUN
                            //{
                            //    IdLUN = (int)dr[0],
                            //    NombreLUN = dr[1].ToString().Trim(),
                            //    DescripcionLUN = dr[2].ToString().Trim(),
                            //    HabilitadoLUN = (bool)dr[3]
                            //};
                            //lstLUN.Add(objLUN);
                        }
                        conn_BD.Close();
                        msjLUN.ListaObjetoInventarios = lstLUN;
                        msjLUN.OperacionExitosa = true;
                    }
                }
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
