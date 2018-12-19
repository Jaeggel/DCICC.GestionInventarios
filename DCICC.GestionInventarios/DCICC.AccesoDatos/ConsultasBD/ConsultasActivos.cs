using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

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
        /// Método para obtener los Activos de la base de datos.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaactivos o activoshabilitados</param>
        /// <returns></returns>
        public MensajesActivos ObtenerActivos(string nombreFuncion)
        {
            List<Activos> lstActivos = new List<Activos>();
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        if(nombreFuncion=="activostotales")
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
                                    ResponsableActivo = dr[12].ToString().Trim(),
                                    EstadoActivo = dr[13].ToString().Trim(),
                                    //Especificaciones adicionales
                                    ExpressServiceCodeActivo = dr[14].ToString().Trim(),
                                    ProductNameActivo = dr[15].ToString().Trim(),
                                    CapacidadActivo = dr[16].ToString().Trim(),
                                    VelocidadTransfActivo = dr[17].ToString().Trim(),
                                    CtActivo = dr[18].ToString().Trim(),
                                    HpePartNumberActivo = dr[19].ToString().Trim(),
                                    CodBarras1Activo = dr[20].ToString().Trim(),
                                    CodBarras2Activo = dr[21].ToString().Trim(),
                                    NumPuertosActivo = dr[22] != DBNull.Value ? (int)dr[22] : 0,
                                    IosVersionActivo = dr[23].ToString().Trim(),
                                    FechaManufacturaActivo = dr[24].ToString().Trim(),
                                    NombreTipoActivo = dr[25].ToString().Trim(),
                                    NombreLaboratorio = dr[26].ToString().Trim(),
                                    NombreMarca = dr[27].ToString().Trim()
                                };
                                lstActivos.Add(objActivos);
                            }
                        }
                        else if(nombreFuncion == "activoscqr")
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
                                    ResponsableActivo = dr[6].ToString().Trim(),
                                    EstadoActivo = dr[7].ToString().Trim(),
                                    NombreTipoActivo = dr[8].ToString().Trim(),
                                    NombreLaboratorio = dr[9].ToString().Trim(),
                                    NombreMarca = dr[10].ToString().Trim(),
                                    ImpresoCQR= (bool)dr[11],
                                };
                                lstActivos.Add(objActivos);
                            }
                        }
                        else if(nombreFuncion == "especificacionesactivos")
                        {
                            while (dr.Read())
                            {
                                Activos objActivos = new Activos
                                {
                                    IdActivo = (int)dr[0],
                                    IdTipoActivo = (int)dr[1],
                                    NombreActivo = dr[2].ToString().Trim(),
                                    ExpressServiceCodeActivo = dr[3].ToString().Trim(),
                                    ProductNameActivo = dr[4].ToString().Trim(),
                                    CapacidadActivo = dr[5].ToString().Trim(),
                                    VelocidadTransfActivo = dr[6].ToString().Trim(),
                                    CtActivo = dr[7].ToString().Trim(),
                                    HpePartNumberActivo = dr[8].ToString().Trim(),
                                    CodBarras1Activo = dr[9].ToString().Trim(),
                                    CodBarras2Activo = dr[10].ToString().Trim(),
                                    NumPuertosActivo = dr[11] != DBNull.Value ? (int)dr[11] : 0,
                                    IosVersionActivo = dr[12].ToString().Trim(),
                                    FechaManufacturaActivo = dr[13].ToString().Trim(),
                                    NombreTipoActivo = dr[14].ToString().Trim(),
                                };
                                lstActivos.Add(objActivos);
                            }
                        }
                        else if (nombreFuncion=="vidautil")
                        {
                            while (dr.Read())
                            {
                                Activos objActivos = new Activos
                                {
                                    IdActivo = (int)dr[0],
                                    IdTipoActivo = (int)dr[1],
                                    IdLaboratorio = (int)dr[2],
                                    NombreActivo = dr[3].ToString().Trim(),
                                    ModeloActivo = dr[4].ToString().Trim(),
                                    SerialActivo = dr[5].ToString().Trim(),
                                    FechaIngresoActivo = DateTime.Parse(dr[6].ToString().Trim()),
                                    ResponsableActivo = dr[7].ToString().Trim(),
                                    EstadoActivo= dr[8].ToString().Trim(),
                                    NombreTipoActivo = dr[9].ToString().Trim(),
                                    NombreLaboratorio = dr[10].ToString().Trim(),
                                    VidaUtilTipoActivo = (int)dr[11],
                                    VidaFinalTipoActivo= DateTime.Parse(dr[12].ToString().Trim())
                                };
                                lstActivos.Add(objActivos);
                            }
                        }
                        conn_BD.Close();
                        msjActivos.ListaObjetoInventarios = lstActivos;
                        msjActivos.OperacionExitosa = true;
                    }
                }
            }
            catch (Exception e)
            {
                conn_BD.Close();
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
                using (NpgsqlCommand cmd = new NpgsqlCommand(nombreFuncion, conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CQR objCQR = new CQR
                            {
                                IdCqr = dr[0].ToString().Trim(),
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
                conn_BD.Close();
                msjCQR.OperacionExitosa = false;
                msjCQR.MensajeError = e.Message;
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para obtener los ID de los CQR.
        /// </summary>
        /// <param name="nombreFuncion">Tipo de función a llamar: consultaCQR</param>
        /// <returns></returns>
        public MensajesCQR ObtenerIdCQR()
        {
            List<CQR> lstCQR = new List<CQR>();
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("consultacqr", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
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
                conn_BD.Close();
                msjCQR.OperacionExitosa = false;
                msjCQR.MensajeError = e.Message;
            }
            return msjCQR;
        }
        /// <summary>
        /// Método para obtener los nombres de los Activos.
        /// </summary>
        /// <returns></returns>
        public MensajesActivos ObtenerNombresActivos()
        {
            List<Activos> lstActivos = new List<Activos>();
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre_detalleact,id_cqr FROM dcicc_detalleactivo", conn_BD))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Activos objActivos = new Activos
                            {
                                NombreActivo = (string)dr[0],
                                IdCQR= (string)dr[1]
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
                conn_BD.Close();
                msjActivos.OperacionExitosa = false;
                msjActivos.MensajeError = e.Message;
            }
            return msjActivos;
        }
    }
}
