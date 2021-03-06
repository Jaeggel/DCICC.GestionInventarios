﻿using DCICC.AccesoDatos.InsercionesBD;
using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DCICC.AccesoDatos.ActualizacionesBD
{
    public class ActualizacionesActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ActualizacionesActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para actualizar un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos ActualizacionActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_detalleactivo SET id_tipoact=@ita, id_marca=@im, id_laboratorio=@il,responsable_detalleact=@rpa, nombre_detalleact=@nda, modelo_detalleact=@mda, serial_detalleact=@sda, fechaingreso_detalleact=@fida, codigoups_detalleact=@cuda, cantidad_detalleact=@cada, descripcion_detalleact=@dsda, estado_detalleact=@eda, expressservicecode_detalleact=@escda, productname_detalleact=@pnda, capacidad_detalleact=@capda, velocidadtransf_detalleact=@vtda, ct_detalleact=@ctda, hpepartnumber_detalleact=@hpnda, codbarras1_detalleact=@cb1da, codbarras2_detalleact=@cb2da, numpuertos_detalleact=@npda, iosversion_detalleact=@ivda, fechamanufactura_detalleact=@fmda WHERE id_detalleact=@ida;", conn_BD))
                {
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdTipoActivo;
                    cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdMarca;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdLaboratorio;
                    cmd.Parameters.Add("rpa", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.ResponsableActivo.Trim();
                    cmd.Parameters.Add("nda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.NombreActivo.Trim();
                    cmd.Parameters.Add("mda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ModeloActivo) ? (object)infoActivo.ModeloActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("sda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.SerialActivo) ? (object)infoActivo.SerialActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("fida", NpgsqlTypes.NpgsqlDbType.Date).Value = !string.IsNullOrEmpty(infoActivo.FechaIngresoActivo.ToLongDateString()) ? (object)infoActivo.FechaIngresoActivo : DBNull.Value;
                    cmd.Parameters.Add("cuda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodigoUpsActivo) ? (object)infoActivo.CodigoUpsActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("cada", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.CantidadActivo;
                    cmd.Parameters.Add("dsda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.DescripcionActivo) ? (object)infoActivo.DescripcionActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("eda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.EstadoActivo.Trim();
                    //Especificaciones Adicionales
                    cmd.Parameters.Add("escda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ExpressServiceCodeActivo) ? (object)infoActivo.ExpressServiceCodeActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("pnda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ProductNameActivo) ? (object)infoActivo.ProductNameActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("capda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CapacidadActivo) ? (object)infoActivo.CapacidadActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("vtda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.VelocidadTransfActivo) ? (object)infoActivo.VelocidadTransfActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("ctda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CtActivo) ? (object)infoActivo.CtActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("hpnda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.HpePartNumberActivo) ? (object)infoActivo.HpePartNumberActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("cb1da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodBarras1Activo) ? (object)infoActivo.CodBarras1Activo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("cb2da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodBarras2Activo) ? (object)infoActivo.CodBarras2Activo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("npda", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoActivo.NumPuertosActivo.ToString()) ? (object)infoActivo.NumPuertosActivo : DBNull.Value;
                    cmd.Parameters.Add("ivda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.IosVersionActivo) ? (object)infoActivo.IosVersionActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("fmda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.FechaManufacturaActivo) ? (object)infoActivo.FechaManufacturaActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdActivo;
                    cmd.ExecuteNonQuery();
                }
                if (infoActivo.EstadoActivo == "DE BAJA")
                {
                    InsercionesHistoricoActivos objInsercionesHA = new InsercionesHistoricoActivos();
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    objInsercionesHA.RegistroHistoricoActivos(infoHistActivo);
                }
                tran.Commit();
                conn_BD.Close();
                msjActivos.OperacionExitosa = true;
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
        /// Método para actualizar el estado de un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos ActualizacionEstadoActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_detalleactivo SET estado_detalleact=@eda WHERE id_detalleact=@ida;", conn_BD))
                {
                    cmd.Parameters.Add("eda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.EstadoActivo.Trim();
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdActivo;
                    cmd.ExecuteNonQuery();
                }
                if (infoActivo.EstadoActivo == "DE BAJA")
                {
                    InsercionesHistoricoActivos objInsercionesHA = new InsercionesHistoricoActivos();
                    HistoricoActivos infoHistActivo = new HistoricoActivos
                    {
                        IdActivo = infoActivo.IdActivo,
                        FechaModifHistActivos = DateTime.Now
                    };
                    objInsercionesHA.RegistroHistoricoActivos(infoHistActivo);
                }
                tran.Commit();
                conn_BD.Close();
                msjActivos.OperacionExitosa = true;
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
        /// Método para actualizar el estado de impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesCQR ActualizacionQR(Activos infoActivo)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_cqr SET impreso_cqr=@imcq WHERE id_cqr=@icq;", conn_BD))
                {
                    cmd.Parameters.Add("imcq", NpgsqlTypes.NpgsqlDbType.Boolean).Value = true;
                    cmd.Parameters.Add("icq", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.IdCQR;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                conn_BD.Close();
                msjCQR.OperacionExitosa = true;
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
        /// Método para actualizar el estado de impreso de un Código QR en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesCQR ActualizacionQR(List<Activos> lstActivos)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                foreach (var item in lstActivos)
                {
                    if (!item.ImpresoCQR)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_cqr SET impreso_cqr=@imcq WHERE id_cqr=@icq;", conn_BD))
                        {
                            cmd.Parameters.Add("imcq", NpgsqlTypes.NpgsqlDbType.Boolean).Value = true;
                            cmd.Parameters.Add("icq", NpgsqlTypes.NpgsqlDbType.Varchar).Value = item.IdCQR;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                tran.Commit();
                conn_BD.Close();
                msjCQR.OperacionExitosa = true;
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
        /// Método para actualizar un Activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos ActualizacionActivoBasico(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                NpgsqlTransaction tran = conn_BD.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE public.dcicc_detalleactivo SET id_laboratorio=@il,nombre_detalleact=@nda, modelo_detalleact=@mda, serial_detalleact=@sda, fechaingreso_detalleact=@fida, codigoups_detalleact=@cuda, estado_detalleact=@eda WHERE id_detalleact=@ida;", conn_BD))
                {
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdLaboratorio;
                    cmd.Parameters.Add("nda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.NombreActivo.Trim();
                    cmd.Parameters.Add("mda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ModeloActivo) ? (object)infoActivo.ModeloActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("sda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.SerialActivo) ? (object)infoActivo.SerialActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("fida", NpgsqlTypes.NpgsqlDbType.Date).Value = !string.IsNullOrEmpty(infoActivo.FechaIngresoActivo.ToLongDateString()) ? (object)infoActivo.FechaIngresoActivo : DBNull.Value;
                    cmd.Parameters.Add("cuda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodigoUpsActivo) ? (object)infoActivo.CodigoUpsActivo.Trim() : DBNull.Value;
                    cmd.Parameters.Add("eda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.EstadoActivo.Trim();
                    cmd.Parameters.Add("ida", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdActivo;
                    cmd.ExecuteNonQuery();
                }
                if (!infoActivo.DeBaja)
                {
                    if (infoActivo.EstadoActivo == "DE BAJA")
                    {
                        InsercionesHistoricoActivos objInsercionesHA = new InsercionesHistoricoActivos();
                        HistoricoActivos infoHistActivo = new HistoricoActivos
                        {
                            IdActivo = infoActivo.IdActivo,
                            FechaModifHistActivos = DateTime.Now
                        };
                        objInsercionesHA.RegistroHistoricoActivos(infoHistActivo);
                    }
                }
                tran.Commit();
                conn_BD.Close();
                msjActivos.OperacionExitosa = true;
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
