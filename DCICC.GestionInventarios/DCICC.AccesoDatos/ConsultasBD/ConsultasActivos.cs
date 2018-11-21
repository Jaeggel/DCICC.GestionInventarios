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
                                IdActivo = int.Parse(dr[0].ToString().Trim()),
                                IdTipoActivo = int.Parse(dr[0].ToString().Trim()),
                                IdCQR = int.Parse(dr[0].ToString().Trim()),
                                IdMarca = int.Parse(dr[0].ToString().Trim()),
                                IdLaboratorio = int.Parse(dr[0].ToString().Trim()),
                                NombreActivo = dr[1].ToString().Trim(),
                                ModeloActivo = dr[1].ToString().Trim(),
                                SerialActivo = dr[1].ToString().Trim(),
                                FechaIngresoActivo = DateTime.Parse(dr[1].ToString().Trim()),
                                CodigoUpsActivo = dr[1].ToString().Trim(),
                                CantidadActivo = int.Parse(dr[0].ToString().Trim()),
                                DescripcionActivo = dr[2].ToString().Trim(),
                                EstadoActivo = dr[2].ToString().Trim(),
                                //Especificaciones adicionales
                                ExpressServiceCodeActivo = dr[2].ToString().Trim(),
                                ProductNameActivo = dr[2].ToString().Trim(),
                                CapacidadActivo = dr[2].ToString().Trim(),
                                VelocidadTransfActivo = dr[2].ToString().Trim(),
                                CtActivo = dr[2].ToString().Trim(),
                                HpePartNumberActivo = dr[2].ToString().Trim(),
                                CodBarras1Activo = dr[2].ToString().Trim(),
                                CodBarras2Activo = dr[2].ToString().Trim(),
                                NumPuertosActivo = int.Parse(dr[0].ToString().Trim()),
                                IosVersionActivo = dr[2].ToString().Trim(),
                                FechaManufacturaActivo = dr[2].ToString().Trim(),
                                NombreTipoActivo = dr[2].ToString().Trim(),
                                NombreMarca = dr[2].ToString().Trim(),
                                NombreLaboratorio = dr[2].ToString().Trim()
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
