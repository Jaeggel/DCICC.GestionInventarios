using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasLaboratorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasLaboratorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Laboratorios de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultalaboratorios o laboratorioshabilitados</param></param>
        /// <returns></returns>
        public MensajesLaboratorios ObtenerLaboratorios(string nombreFuncion)
        {
            List<Laboratorios> lstLaboratorios = new List<Laboratorios>();
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Laboratorios objLaboratorios = new Laboratorios
                            {
                                IdLaboratorio= (int)dr[0],
                                NombreLaboratorio = dr[1].ToString().Trim(),
                                UbicacionLaboratorio= dr[2].ToString().Trim(),
                                DescripcionLaboratorio = dr[3].ToString().Trim(),
                                HabilitadoLaboratorio = (bool)dr[4]
                            };
                            lstLaboratorios.Add(objLaboratorios);
                        }
                        conn_BD.Close();
                        msjLaboratorios.ListaObjetoInventarios = lstLaboratorios;
                        msjLaboratorios.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = false;
                msjLaboratorios.MensajeError = e.Message;
            }
            return msjLaboratorios;
        }
    }
}
