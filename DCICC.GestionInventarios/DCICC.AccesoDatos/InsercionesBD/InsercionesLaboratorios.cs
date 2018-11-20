using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesLaboratorios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesLaboratorios()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo laboratorio en la base de datos.
        /// </summary>
        /// <param name="infoLaboratorio"></param>
        /// <returns></returns>
        public MensajesLaboratorios RegistroLaboratorio(Laboratorios infoLaboratorio)
        {
            MensajesLaboratorios msjLaboratorios = new MensajesLaboratorios();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_laboratorios (nombre_laboratorio,ubicacion_laboratorio,descripcion_laboratorio,habilitado_laboratorio) VALUES (@nl,@ul,@dl,@hl)", conn_BD))
                {
                    cmd.Parameters.Add("nl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLaboratorio.NombreLaboratorio;
                    cmd.Parameters.Add("ul", NpgsqlTypes.NpgsqlDbType.Varchar).Value = infoLaboratorio.UbicacionLaboratorio;
                    cmd.Parameters.Add("dl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoLaboratorio.DescripcionLaboratorio) ? (object)infoLaboratorio.DescripcionLaboratorio : DBNull.Value;
                    cmd.Parameters.Add("hl", NpgsqlTypes.NpgsqlDbType.Boolean).Value = infoLaboratorio.HabilitadoLaboratorio;
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjLaboratorios.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjLaboratorios.OperacionExitosa = false;
                msjLaboratorios.MensajeError = e.Message;
            }
            return msjLaboratorios;
        }
    }
}
