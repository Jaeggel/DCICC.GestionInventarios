using DCICC.Entidades.EntidadesInventarios;
using DCICC.Entidades.MensajesInventarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCICC.AccesoDatos.InsercionesBD
{
    public class InsercionesRoles
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public InsercionesRoles()
        {
            conn_BD = ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para ingresar un nuevo rol en la base de datos.
        /// </summary>
        /// <returns></returns>
        public MensajesRoles RegistroRol(Roles infoRol)
        {
            MensajesRoles msjRoles = new MensajesRoles();
            try
            {
                using (var cmd = new NpgsqlCommand("insert into dcicc_roles (nombre_rol,descripcion_rol,habilitado_rol) VALUES (@nr,@dr,@hr)", conn_BD))
                {
                    cmd.Parameters.AddWithValue("nr", infoRol.NombreRol);
                    cmd.Parameters.Add("dr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = !String.IsNullOrEmpty(infoRol.DescripcionRol) ? (object)infoRol.DescripcionRol : DBNull.Value;
                    cmd.Parameters.AddWithValue("hr", infoRol.HabilitadoRol);
                    cmd.ExecuteNonQuery();
                }
                conn_BD.Close();
                msjRoles.OperacionExitosa = true;
            }
            catch (Exception e)
            {
                msjRoles.OperacionExitosa = false;
                msjRoles.MensajeError = e.Message;
            }
            return msjRoles;
        }
    }
}
