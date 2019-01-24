using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasMaqVirtuales
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasMaqVirtuales()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener las Máquinas Virtuales de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaMaqVirtuales o maqvirtualeshabilitados</param>
        /// <returns></returns>
        public MensajesMaqVirtuales ObtenerMaqVirtuales(string nombreFuncion)
        {
            List<MaqVirtuales> lstMaqVirtuales = new List<MaqVirtuales>();
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MaqVirtuales objMaqVirtuales = new MaqVirtuales
                            {
                                IdMaqVirtuales = (int)dr[0],
                                IdSistOperativos= (int)dr[1],
                                IdLUN = (int)dr[2],
                                UsuarioMaqVirtuales = dr[3].ToString().Trim(),
                                NombreMaqVirtuales = dr[4].ToString().Trim(),
                                PropositoMaqVirtuales = dr[5].ToString().Trim(),
                                DireccionIPMaqVirtuales = dr[6].ToString().Trim(),
                                DiscoMaqVirtuales = dr[7].ToString().Trim(),
                                RamMaqVirtuales = (int)dr[8],
                                DescripcionMaqVirtuales = dr[9].ToString().Trim(),
                                HabilitadoMaqVirtuales = (bool)dr[10],
                                NombreSistOperativos = dr[11].ToString().Trim(),
                                NombreLUN = dr[12].ToString().Trim(),
                                FechaCreacionMaqVirtuales = dr[13] != DBNull.Value ? DateTime.Parse(dr[13].ToString().Trim()) : new DateTime(DateTime.Now.Year, 1, 1),
                                FechaExpiracionMaqVirtuales = dr[14] != DBNull.Value ? DateTime.Parse(dr[14].ToString().Trim()) : new DateTime(DateTime.Now.Year, 1, 1),
                            };
                            string[] capacidadTemp = objMaqVirtuales.DiscoMaqVirtuales.Split(new char[0]);
                            if (capacidadTemp.Length == 2)
                            {
                                objMaqVirtuales.SizeMaqVirtuales = int.Parse(capacidadTemp[0]);
                                objMaqVirtuales.UnidadMaqVirtuales = capacidadTemp[1];
                            }
                            else
                            {
                                objMaqVirtuales.SizeMaqVirtuales = int.Parse(capacidadTemp[0]);
                            }
                            lstMaqVirtuales.Add(objMaqVirtuales);
                        }
                        conn_BD.Close();
                        msjMaqVirtuales.ListaObjetoInventarios = lstMaqVirtuales;
                        msjMaqVirtuales.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjMaqVirtuales.OperacionExitosa = false;
                msjMaqVirtuales.MensajeError = e.Message;
            }
            return msjMaqVirtuales;
        }
    }
}
