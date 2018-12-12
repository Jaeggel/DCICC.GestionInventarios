using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasStorage
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasStorage()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los Storage de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaStorage o Storagehabilitados</param></param>
        /// <returns></returns>
        public MensajesStorage ObtenerStorage(string nombreFuncion)
        {
            List<Storage> lstStorage = new List<Storage>();
            MensajesStorage msjStorage = new MensajesStorage();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Storage objStorage = new Storage
                            {
                                IdStorage = (int)dr[0],
                                NombreStorage = dr[1].ToString().Trim(),
                                NickStorage= dr[2].ToString().Trim(),
                                CapacidadStorage= dr[3].ToString().Trim(),
                                DescripcionStorage = dr[4].ToString().Trim(),
                                HabilitadoStorage = (bool)dr[5]
                            };
                            string[] capacidadTemp = objStorage.CapacidadStorage.Split(new char[0]);
                            objStorage.SizeStorage =int.Parse(capacidadTemp[0]);
                            objStorage.UnidadStorage = capacidadTemp[1];
                            lstStorage.Add(objStorage);
                        }
                        conn_BD.Close();
                        msjStorage.ListaObjetoInventarios = lstStorage;
                        msjStorage.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjStorage.OperacionExitosa = false;
                msjStorage.MensajeError = e.Message;
            }
            return msjStorage;
        }
    }
}
