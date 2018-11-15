using DCICC.Entidades.EntidadesInventarios;
using DCICC.Seguridad.Encryption;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DCICC.AccesoDatos.ConsultasBD
{
    public class ConsultasUsuarios
    {
        NpgsqlConnection conn_BD = null;
        /// <summary>
        /// Constructor para realizar llamar al método de conexión con la base de datos.
        /// </summary>
        public ConsultasUsuarios()
        {
            conn_BD=ConfigBaseDatos.ConnectDB();
        }
        /// <summary>
        /// Método para obtener todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> ObtenerUsuarios(string nombreFuncion)
        {
            List<Usuarios> lstUsuarios = new List<Usuarios>();
            try
            {
                using (var cmd = new NpgsqlCommand("consultaUsuarios", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuarios objUsuarios = new Usuarios
                            {
                                IdUsuario = Int32.Parse(dr[0].ToString()),
                                IdRol = Int32.Parse(dr[1].ToString().Trim()),
                                NombresUsuario = dr[2].ToString().Trim(),
                                NickUsuario = dr[3].ToString().Trim(),
                                PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                CorreoUsuario = dr[5].ToString().Trim(),
                                TelefonoUsuario = dr[6].ToString().Trim(),
                                TelefonoCelUsuario = dr[7].ToString().Trim(),
                                DireccionUsuario = dr[8].ToString().Trim(),
                                HabilitadoUsuario = Boolean.Parse(dr[9].ToString().Trim())
                            };
                            lstUsuarios.Add(objUsuarios);
                        }
                        conn_BD.Close();
                    }
                }                
            }
            catch (Exception)
            {
                lstUsuarios = null;
            }
            return lstUsuarios;
        }
        /// <summary>
        /// Método para obtener todos la funcion UsuariosRoles de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> ObtenerUsuariosRoles()
        {
            List<Usuarios> lstUsuariosRoles = new List<Usuarios>();
            try
            {
                using (var cmd = new NpgsqlCommand("usuariosRoles", conn_BD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuarios objUsuariosRoles = new Usuarios
                            {
                                IdUsuario = Int32.Parse(dr[0].ToString()),
                                IdRol = Int32.Parse(dr[1].ToString().Trim()),
                                NombresUsuario = dr[2].ToString().Trim(),
                                NickUsuario = dr[3].ToString().Trim(),
                                PasswordUsuario = ConfigEncryption.DesencriptarValor(dr[4].ToString().Trim()),
                                CorreoUsuario = dr[5].ToString().Trim(),
                                TelefonoUsuario = dr[6].ToString().Trim(),
                                TelefonoCelUsuario = dr[7].ToString().Trim(),
                                DireccionUsuario = dr[8].ToString().Trim(),
                                HabilitadoUsuario = Boolean.Parse(dr[9].ToString().Trim()),
                                NombreRol = dr[1].ToString().Trim()
                            };
                            lstUsuariosRoles.Add(objUsuariosRoles);
                        }
                        conn_BD.Close();
                    }
                }
            }
            catch (Exception)
            {
                lstUsuariosRoles = null;
            }
            return lstUsuariosRoles;
        }
    }
}
