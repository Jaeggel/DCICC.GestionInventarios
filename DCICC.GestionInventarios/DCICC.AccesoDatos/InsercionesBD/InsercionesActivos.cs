﻿using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesActivos
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesActivos()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nueva activo en la base de datos.
        /// </summary>
        /// <param name="infoActivo"></param>
        /// <returns></returns>
        public MensajesActivos RegistroActivo(Activos infoActivo)
        {
            MensajesActivos msjActivos = new MensajesActivos();
            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO public.dcicc_detalleactivo(id_tipoact, id_cqr, id_marca, id_laboratorio, nombre_detalleact, modelo_detalleact, serial_detalleact, fechaingreso_detalleact, codigoups_detalleact, cantidad_detalleact, descripcion_detalleact, estado_detalleact, expressservicecode_detalleact, productname_detalleact, capacidad_detalleact, velocidadtransf_detalleact, ct_detalleact, hpepartnumber_detalleact, codbarras1_detalleact, codbarras2_detalleact, numpuertos_detalleact, iosversion_detalleact, fechamanufactura_detalleact)VALUES (@ita, @icq,@im, @il,@nda,@mda, @sda, @fida, @cuda,@cada, @dsda, @eda,@escda, @pnda, @capda,@vtda, @ctda, @hpnda,@cb1da, @cb2da, @npda,@ivda, @fmda);", conn_BD))
                {
                    cmd.Parameters.Add("ita", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdTipoActivo;
                    cmd.Parameters.Add("icq", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.IdCQR;
                    cmd.Parameters.Add("im", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdMarca;
                    cmd.Parameters.Add("il", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.IdLaboratorio;
                    cmd.Parameters.Add("nda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.NombreActivo;
                    cmd.Parameters.Add("mda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ModeloActivo) ? (object)infoActivo.ModeloActivo : DBNull.Value;
                    cmd.Parameters.Add("sda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.SerialActivo) ? (object)infoActivo.SerialActivo : DBNull.Value;
                    cmd.Parameters.Add("fida", NpgsqlTypes.NpgsqlDbType.Date).Value = !string.IsNullOrEmpty(infoActivo.FechaIngresoActivo.ToLongDateString()) ? (object)infoActivo.FechaIngresoActivo : DBNull.Value;
                    cmd.Parameters.Add("cuda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodigoUpsActivo) ? (object)infoActivo.CodigoUpsActivo : DBNull.Value;
                    cmd.Parameters.Add("cada", NpgsqlTypes.NpgsqlDbType.Integer).Value = infoActivo.CantidadActivo;
                    cmd.Parameters.Add("dsda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.DescripcionActivo) ? (object)infoActivo.DescripcionActivo : DBNull.Value;
                    cmd.Parameters.Add("eda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoActivo.EstadoActivo;
                    //Especificaciones Adicionales
                    cmd.Parameters.Add("escda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ExpressServiceCodeActivo) ? (object)infoActivo.ExpressServiceCodeActivo : DBNull.Value;
                    cmd.Parameters.Add("pnda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.ProductNameActivo) ? (object)infoActivo.ProductNameActivo : DBNull.Value;
                    cmd.Parameters.Add("capda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CapacidadActivo) ? (object)infoActivo.CapacidadActivo : DBNull.Value;
                    cmd.Parameters.Add("vtda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.VelocidadTransfActivo) ? (object)infoActivo.VelocidadTransfActivo : DBNull.Value;
                    cmd.Parameters.Add("ctda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CtActivo) ? (object)infoActivo.CtActivo : DBNull.Value;
                    cmd.Parameters.Add("hpnda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.HpePartNumberActivo) ? (object)infoActivo.HpePartNumberActivo : DBNull.Value;
                    cmd.Parameters.Add("cb1da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodBarras1Activo) ? (object)infoActivo.CodBarras1Activo : DBNull.Value;
                    cmd.Parameters.Add("cb2da", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.CodBarras2Activo) ? (object)infoActivo.CodBarras2Activo : DBNull.Value;
                    cmd.Parameters.Add("npda", NpgsqlTypes.NpgsqlDbType.Integer).Value = !string.IsNullOrEmpty(infoActivo.NumPuertosActivo.ToString()) ? (object)infoActivo.NumPuertosActivo : DBNull.Value;
                    cmd.Parameters.Add("ivda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.IosVersionActivo) ? (object)infoActivo.IosVersionActivo : DBNull.Value;
                    cmd.Parameters.Add("fmda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !string.IsNullOrEmpty(infoActivo.FechaManufacturaActivo) ? (object)infoActivo.FechaManufacturaActivo : DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjActivos.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjActivos.OperacionExitosa = false;
                msjActivos.MensajeError = e.Message;
            }
            return msjActivos;
        }
        /// <summary>
        /// Método para ingresar un nuevo CQR en la base de datos.
        /// </summary>
        /// <param name="infoCQR"></param>
        /// <returns></returns>
        public MensajesCQR RegistroCQR(CQR infoCQR)
        {
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                //falta valor boolean
                using (var cmd = new NpgsqlCommand("insert into dcicc_CQR (id_CQR,datos_CQR) VALUES (@ic,@dc)", conn_BD))
                {
                    cmd.Parameters.Add("ic", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoCQR.IdCqr;
                    cmd.Parameters.Add("dc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoCQR.Bytea;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjCQR.OperacionExitosa = true;
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