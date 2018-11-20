using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasDetalleActivo
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasDetalleActivo()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los CQR de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaCQR</param>
        /// <returns></returns>
        public MensajesCQR ObtenerCQR(string nombreFuncion)
        {
            List<CQR> lstCQR = new List<CQR>();
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CQR objCQR = new CQR
                            {
                                IdCqr = int.Parse(dr[0].ToString().Trim()),
                                //Bytea = byte[].dr[1].ToString().Trim(),
                            };
                            lstCQR.Add(objCQR);
                        }
                        conn_BD.Close();
                        msjCQR.ListaObjetoInventarios = lstCQR;
                        msjCQR.OperacionExitosa = true;
                    }
                }
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
