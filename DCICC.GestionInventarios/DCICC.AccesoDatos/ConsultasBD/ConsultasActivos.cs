using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener los activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaactivos o activoshabilitados</param>
        /// <returns></returns>
        public MensajesActivos ObtenerActivos(string nombreFuncion)
        {
            List<Activos> lstActivos = new List<Activos>();
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                using (var cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Activos objActivos = new Activos
                            {
                                IdActivo = (int)dr[0],
                                IdTipoActivo = (int)dr[1],
                                IdCQR = (string)dr[2],
                                IdMarca = (int)dr[3],
                                IdLaboratorio = (int)dr[4],
                                NombreActivo = dr[5].ToString().Trim(),
                                ModeloActivo = dr[6].ToString().Trim(),
                                SerialActivo = dr[7].ToString().Trim(),
                                FechaIngresoActivo = DateTime.Parse(dr[8].ToString().Trim()),
                                CodigoUpsActivo = dr[9].ToString().Trim(),
                                CantidadActivo = (int)dr[10],
                                DescripcionActivo = dr[11].ToString().Trim(),
                                EstadoActivo = dr[12].ToString().Trim(),
                                //Especificaciones adicionales
                                ExpressServiceCodeActivo = dr[13].ToString().Trim(),
                                ProductNameActivo = dr[14].ToString().Trim(),
                                CapacidadActivo = dr[15].ToString().Trim(),
                                VelocidadTransfActivo = dr[16].ToString().Trim(),
                                CtActivo = dr[17].ToString().Trim(),
                                HpePartNumberActivo = dr[18].ToString().Trim(),
                                CodBarras1Activo = dr[19].ToString().Trim(),
                                CodBarras2Activo = dr[20].ToString().Trim(),
                                NumPuertosActivo = dr[21] != DBNull.Value ? (int)dr[21] : 0,
                                IosVersionActivo = dr[22].ToString().Trim(),
                                FechaManufacturaActivo = dr[23].ToString().Trim(),
                                NombreTipoActivo = dr[24].ToString().Trim(),
                                NombreMarca = dr[25].ToString().Trim(),
                                NombreLaboratorio = dr[26].ToString().Trim()
                            };
                            lstActivos.Add(objActivos);
                        }
                        conn_BD.Close();
                        msjActivos.ListaObjetoInventarios = lstActivos;
                        msjActivos.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                msjActivos.OperacionExitosa = false;
                msjActivos.MensajeError = e.Message;
            }
            return msjActivos;
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
                                IdCqr = dr[0].ToString().Trim(),
                                Bytea = (byte[])dr[1],
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
        /// <summary>
        /// Método para obtener los id de los CQR.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaCQR</param>
        /// <returns></returns>
        public MensajesCQR ObtenerIdCQR()
        {
            List<CQR> lstCQR = new List<CQR>();
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                using (var cmd = new NpgsqlCommand("consultacqr", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())//obtener tamaño
                        {
                            CQR objCQR = new CQR
                            {
                                IdCqr = dr[0].ToString().Trim()
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
