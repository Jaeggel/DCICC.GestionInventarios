using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        /// Método para obtener las MaqVirtuales de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaMaqVirtuales o maqvirtualeshabilitados</param>
        /// <returns></returns>
        public MensajesMaqVirtuales ObtenerMaqVirtuales(string nombreFuncion)
        {
            List<MaqVirtuales> lstMaqVirtuales = new List<MaqVirtuales>();
            MensajesMaqVirtuales msjMaqVirtuales = new MensajesMaqVirtuales();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MaqVirtuales objMaqVirtuales = new MaqVirtuales
                            {
                                IdMaqVirtuales = int.Parse(dr[0].ToString().Trim()),
                                NombreSistOperativo = dr[1].ToString().Trim(),
                                UsuarioMaqVirtuales = dr[2].ToString().Trim(),
                                NombreMaqVirtuales = dr[3].ToString().Trim(),
                                PropositoMaqVirtuales = dr[4].ToString().Trim(),
                                DireccionIPMaqVirtuales = dr[5].ToString().Trim(),
                                DiscoMaqVirtuales = int.Parse(dr[6].ToString().Trim()),
                                RamMaqVirtuales = int.Parse(dr[7].ToString().Trim()),
                                DescripcionMaqVirtuales = dr[8].ToString().Trim(),
                                HabilitadoMaqVirtuales = bool.Parse(dr[9].ToString().Trim())
                            };
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
                msjMaqVirtuales.OperacionExitosa = false;
                msjMaqVirtuales.MensajeError = e.Message;
            }
            return msjMaqVirtuales;
        }
    }
}
